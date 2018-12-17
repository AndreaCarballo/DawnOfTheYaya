using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAgent : MonoBehaviour
{
    //global variables
    public GameObject player;
    Rigidbody rb;
    private NavMeshAgent nav;
    System.Random random = new System.Random();
    public string path;
    public string pathHearing;
    private Animator anim;
    public int ID;
    private Game diff;

    //movement variables
    [HideInInspector]
    public float walkingSpeed;
    private int currentAction;
    private float lastDistance = 0f;
    private Vector3 lastPosition;
    private Vector3 lastPositionHeard;
    private bool seen = false;
    private bool heard = false;
    private bool turn = false;
    private int countTurn = 0;
    private bool gotHearingAction = false;
    private int go = 0;
    private bool zombieLeftPast = false;
    private bool zombieRightPast = false;
    private float soundType;
    private float lastSoundType = 0f;

    //patrol variables
    public Transform objetivo;

    //training variables
    private bool restartPostion = false;

    //reward variables
    QLearning QL; //3 possible actions
    QLearning QLHearing; //2 possible actions
    private bool positiveTrigger = false;
    public float positiveReward = 10f; //reward when it hits the player
    public float negativeReward = -10f;
    public float timePunishment = -0.5f; //punishment for each update
    public float distanceReward = 1f; //reward when it gets close
    private float currentReward = 0f;
    private float rewardHearing = 0f;
    private float wrongSound = -8f;
    private float rightSound = 11f;
    public float distanceZombies = 3f;

    //FOV variables
    public float FOVRadius; //how far ahead the agent can see
    public float hearingRadius;
    [Range(0, 360)]
    public float FOVAngle; //angle of the FOV cone
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;
    public LayerMask enemyLayer;
    public LayerMask soundLayer;
    public float FOVMeshResolution; //how many rays we cast out to draw the FOV per degree
    public int edgeResolveIterations; //number of iterations to find the closest point to the edge of an obstacle
    public float edgeDistance; //minimum distance between hits to do edge detection and avoid problems when the rays hit two different obstacles
    public MeshFilter FOVMeshFilter;
    Mesh FOVMesh;



    // Use this for initialization
    void Start()
    {
        diff = GameObject.Find("Canvas").GetComponent<Game>();
        QL = new QLearning(3, path, false);
        QLHearing = new QLearning(2, pathHearing, true);
        applyDifficultySettings();
        rb = GetComponent<Rigidbody>();
        FOVMesh = new Mesh();
        FOVMesh.name = "FOV Mesh";
        FOVMeshFilter.mesh = FOVMesh;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

    }

    public void applyDifficultySettings()
    {
        switch (diff.difficulty)
        {
            case 0:
                walkingSpeed = 10f;
                wrongSound = -5f; //the user will be able to use each object 5 times
                break;
            case 1:
                walkingSpeed = 20f;
                wrongSound = -8f; //the user will be able to use each object 4 times
                break;
            case 2:
                walkingSpeed = 30f;
                wrongSound = -11f; //the user will be able to use each object 3 times
                break;
        }

        QL.changePath(path);
        QLHearing.changePath(pathHearing);
    }

    void FixedUpdate()
    {

        bool inView = false;
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, FOVRadius, playerLayer);
        if (targetsInViewRadius.Length != 0)
        {
            Transform target = targetsInViewRadius[0].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < FOVAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleLayer))
                {
                    inView = true;
                    seen = true;
                    AgentBehaviorSight(true);

                }
            }
        }


        if (!inView)
        {
            Collider[] hearingObjects = Physics.OverlapSphere(transform.position, hearingRadius, soundLayer | playerLayer);
            for (int j = 0; j < hearingObjects.Length; j++)
            {
                float hearingDistance = HearingRange(hearingObjects[j].transform.position);
                AudioSource audioTarget = GameObject.Find(hearingObjects[j].name).GetComponent<AudioSource>();
                if (hearingDistance < hearingRadius && audioTarget.isPlaying)
                {
                    heard = true;
                    switch (hearingObjects[j].name)
                    {
                        case "Player":
                            soundType = 1f;
                            break;
                        case "CrashBottle":
                            soundType = 2f;
                            break;
                        case "Rock":
                            soundType = 3f;
                            break;
                    }
                    lastPositionHeard = hearingObjects[j].transform.position;
                    break;
                }
            }



            if (heard)
            {
                AgentBehaviorHearing();
            }
            else if (seen)
            {
                AgentBehaviorSight(false);
            }
            else
            {
                if (turn)
                {
                    if (countTurn <= 700)
                    {
                        countTurn++;
                    }
                    else
                    {
                        countTurn = 0;
                        turn = false;
                        anim.SetTrigger("Idle");
                    }

                }
                else
                {
                    Patrol();
                }

            }


        }






    }

    void LateUpdate()
    {
        if (diff.difficulty != 2)
        {
            DrawFieldOfView();
        } else
        {
            FOVMesh.Clear();
        }
        
    }






    void Patrol()
    {
        anim.SetTrigger("Patrol");
        transform.LookAt(objetivo.position + Vector3.up * transform.position.y); //look at player's last position
        Vector3 point = Vector3.MoveTowards(transform.position, objetivo.position, walkingSpeed * Time.fixedDeltaTime);
        nav.SetDestination(point);
    }


    void AgentBehaviorSight(bool inView)
    {
        if (inView)
        {
            if (heard)
            {
                gotHearingAction = false;
                lastSoundType = 0;
                heard = false;
                rewardHearing += rightSound;
                float[] qStateHear = new float[4];
                qStateHear[0] = 0f;
                qStateHear[1] = 0f;
                qStateHear[2] = 0f;
                qStateHear[3] = 0f;
                QLHearing.getAction(qStateHear, rewardHearing);
                rewardHearing = 0f;
            }
            lastPosition = player.transform.position;
            transform.LookAt(player.transform.position + Vector3.up * transform.position.y);
            //The state of the world is the relative position of the zombie to the player
            Vector3 relativePositionToPlayer = player.transform.InverseTransformPoint(transform.position);
            relativePositionToPlayer.x = (float)Math.Round((double)relativePositionToPlayer.x, 1);
            relativePositionToPlayer.z = (float)Math.Round((double)relativePositionToPlayer.z, 1);
            float[] qState = new float[4];

            if (Math.Abs(relativePositionToPlayer.x) < 3f && Math.Abs(relativePositionToPlayer.x) >= 0f)
            {
                qState[0] = 1f;
            }
            else if (Math.Abs(relativePositionToPlayer.x) < 6f && Math.Abs(relativePositionToPlayer.x) >= 3f)
            {
                qState[0] = 2f;
            }
            else if (Math.Abs(relativePositionToPlayer.x) < 10f && Math.Abs(relativePositionToPlayer.x) >= 6f)
            {
                qState[0] = 3f;
            }

            if (Math.Abs(relativePositionToPlayer.z) < 3f && Math.Abs(relativePositionToPlayer.z) >= 0f)
            {
                qState[1] = 1f;
            }
            else if (Math.Abs(relativePositionToPlayer.z) < 6f && Math.Abs(relativePositionToPlayer.z) >= 3f)
            {
                qState[1] = 2f;
            }
            else if (Math.Abs(relativePositionToPlayer.z) < 10f && Math.Abs(relativePositionToPlayer.z) >= 6f)
            {
                qState[1] = 3f;
            }

            bool zombieLeft = false;
            bool zombieRight = false;

            Collider[] enemiesAround = Physics.OverlapSphere(transform.position, distanceZombies, enemyLayer);
            for (int i = 0; i < enemiesAround.Length; i++)
            {
                Transform target = enemiesAround[i].transform;
                if (target.transform.root != transform)
                {
                    Vector3 dirToTarget = (target.position - transform.position).normalized;
                    if (Vector3.Angle(transform.forward, dirToTarget) < 90)
                    {
                        float dstToTarget = Vector3.Distance(transform.position, target.position);
                        if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleLayer))
                        {
                            Vector3 relativeToMe = transform.InverseTransformPoint(target.position);
                            if (relativeToMe.x < 0)
                            {
                                zombieLeft = true;
                                qState[2] = 1f;
                            }
                            else
                            {
                                zombieRight = true;
                                qState[3] = 1f;
                            }
                        }
                    }
                }

            }


            if (!zombieLeft)
            {
                qState[2] = 0f;
            }
            if (!zombieRight)
            {
                qState[3] = 0f;
            }



            currentAction = QL.getAction(qState, currentReward); //gets the optimal action from q-learning
            currentReward = 0f; //reinitializes reward

            Move(currentAction); //moves the zombie

            if (zombieRightPast && !zombieRight)
            {
                currentReward += distanceReward * 2;
            }
            else if (zombieRightPast && zombieRight)
            {
                currentReward += timePunishment;
            }

            if (zombieLeftPast && !zombieLeft)
            {
                currentReward += distanceReward * 2;
            }
            else if (zombieLeftPast && zombieLeft)
            {
                currentReward += timePunishment;
            }

            zombieLeftPast = zombieLeft;
            zombieRightPast = zombieRight;

            if (positiveTrigger) //checks if it hit the player
            {
                currentReward += positiveReward;
            }


            float currentDistance = (float)Math.Round((double)Vector3.Distance(transform.position, player.transform.position), 2);

            if (currentDistance < lastDistance) //if the agent got closer to the player it'll get a reward
            {
                if (lastDistance - currentDistance < 2)
                {
                    currentReward += distanceReward;
                }
                else
                {
                    currentReward += distanceReward * 2;
                }

            }
            else
            {
                currentReward += timePunishment;
            }

            lastDistance = currentDistance;
            rb.velocity = Vector3.zero;
        }

        //if (restartPostion) //for training
        //{
        //    //Patrol();
        //    //rb.MovePosition(new Vector3(xStartPosition, yStartPosition, zStartPosition));
        //    //transform.rotation = new Quaternion(0, 90, 0, 0);
        //    //transform.position = new Vector3(xStartPosition, yStartPosition, zStartPosition);
        //    //player.transform.position = new Vector3(random.Next(-xPlayer, xPlayer), yStartPosition, random.Next(-zPlayer, zPlayer));
        //    lastPosition = player.transform.position;
        //    lastDistance = 0f;
        //    restartPostion = false;
        //    seen = false;
        //    heard = false;
        //    Debug.Log("restarting position");
        //}
        //else

        if (seen && !inView)
        {
            currentReward += negativeReward;
            float distSee = Vector3.Distance(lastPosition, transform.position);
            if (distSee > 1f)
            {
                Debug.Log("go to last position");
                transform.LookAt(lastPosition + Vector3.up * transform.position.y); //look at player's last position
                Vector3 point = Vector3.MoveTowards(transform.position, lastPosition, walkingSpeed * Time.fixedDeltaTime);
                anim.SetTrigger("Walk");
                nav.SetDestination(point);
            }
            else
            {
                anim.SetTrigger("Idle");
                anim.SetTrigger("TurnRight");
                Debug.Log("end seen");
                seen = false;
                turn = true;
            }
        }


    }

    void AgentBehaviorHearing()
    {
        float distHear = Vector3.Distance(lastPositionHeard, transform.position);
        if (distHear > 1f)
        {
            if (!gotHearingAction || soundType != lastSoundType)
            {
                lastSoundType = soundType;
                gotHearingAction = true;
                float[] qState = new float[4];
                qState[0] = soundType;
                qState[1] = 0f;
                qState[2] = 0f;
                qState[3] = 0f;
                go = QLHearing.getAction(qState, rewardHearing);
                rewardHearing = 0f;
                Debug.Log("go or not");
            }

            if (go == 1)
            {
                Debug.Log("go to hearing");
                transform.LookAt(lastPositionHeard + Vector3.up * transform.position.y); //look at player's last position
                Vector3 point = Vector3.MoveTowards(transform.position, lastPositionHeard, walkingSpeed * Time.fixedDeltaTime);
                anim.SetTrigger("Walk");
                nav.SetDestination(point);
            }
            else
            {
                gotHearingAction = false;
                lastSoundType = 0;
                anim.SetTrigger("Idle");
                Debug.Log("didnt go");
                heard = false;
            }

        }
        else
        {
            gotHearingAction = false;
            lastSoundType = 0;
            rewardHearing += wrongSound;
            float[] qState = new float[4];
            qState[0] = 0f;
            qState[1] = 0f;
            qState[2] = 0f;
            qState[3] = 0f;
            QLHearing.getAction(qState, rewardHearing);
            rewardHearing = 0f;
            anim.SetTrigger("Idle");
            anim.SetTrigger("TurnRight");
            Debug.Log("end heard");
            turn = true;
            heard = false;
            seen = false;
        }

    }



    void DrawFieldOfView()
    {
        int rayCount = Mathf.RoundToInt(FOVAngle * FOVMeshResolution); //number of rays
        float rayAngle = FOVAngle / rayCount;
        List<Vector3> rayPoints = new List<Vector3>(); //list of points the rays hit to construct the mesh

        //we need to save the old raycast every time to check if it hit an obstacle and find the edges
        RayCastInfo oldRayCast = new RayCastInfo();
        for (int i = 0; i <= rayCount; i++)
        {
            float angle = transform.eulerAngles.y - FOVAngle / 2 + rayAngle * i; //current global angle
            RayCastInfo newRayCast = LaunchRayCast(angle); //casts the ray

            if (i > 0)
            {
                bool edgeDstThresholdExceeded = Mathf.Abs(oldRayCast.distance - newRayCast.distance) > edgeDistance;

                //one of them hit an obstacle and the other one didn't, that means there's an edge
                if (oldRayCast.hit != newRayCast.hit || (oldRayCast.hit && newRayCast.hit && edgeDstThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldRayCast, newRayCast); //find the new edge

                    //add the new points to the mesh
                    if (edge.pointA != Vector3.zero)
                    {
                        rayPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        rayPoints.Add(edge.pointB);
                    }
                }

            }


            rayPoints.Add(newRayCast.point); //adds the point to the mesh list
            oldRayCast = newRayCast;
        }

        int vertexCount = rayPoints.Count + 1; //the number of rays plus the zombie location
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3]; //the number of triangles is (vertexCount - 2) and each triangle is described by 3 numbers

        vertices[0] = Vector3.zero; //the first one is the zombie's position
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(rayPoints[i]); //points relative to the zombie

            if (i < vertexCount - 2) //number of triangles
            {
                triangles[i * 3] = 0; //origin of each triangle
                triangles[i * 3 + 1] = i + 1; //second vertex
                triangles[i * 3 + 2] = i + 2; //last vertex
            }
        }

        //restart mesh
        FOVMesh.Clear();

        FOVMesh.vertices = vertices;
        FOVMesh.triangles = triangles;
        FOVMesh.RecalculateNormals();
    }

    /*
    It casts a raycast and returns the relevant information depending on whether it hits an obstacle or not 
    globalAngle: the angle at which to cast the ray
    */
    RayCastInfo LaunchRayCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, FOVRadius, obstacleLayer)) //if it hit an obstacle
        {
            return new RayCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new RayCastInfo(false, transform.position + dir * FOVRadius, FOVRadius, globalAngle);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    EdgeInfo FindEdge(RayCastInfo minViewCast, RayCastInfo maxViewCast)
    {
        //the edge will be between these points
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            //we cast out a ray between the two points
            float angle = (minAngle + maxAngle) / 2;
            RayCastInfo newViewCast = LaunchRayCast(angle);

            bool edgeDistanceExceeded = Mathf.Abs(minViewCast.distance - newViewCast.distance) > edgeDistance;

            //we update either the min angle or the max angle according to the result of the new raycast
            if (newViewCast.hit == minViewCast.hit && !edgeDistanceExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }

    public float HearingRange(Vector3 position)
    {
        NavMeshPath path = new NavMeshPath();

        if (nav.enabled)
        {
            nav.CalculatePath(position, path);
        }

        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length - 1] = position;

        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        float pathLength = 0f;

        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLength;
    }



    void OnTriggerEnter(Collider col)
    {
        // reaction to positive reward
        if (col.gameObject.name == "Player")
        {
            positiveTrigger = true;
            restartPostion = true;
        }

    }


    void OnTriggerExit(Collider col)
    {
        // reaction to positive reward
        if (col.gameObject.name == "Player")
        {
            positiveTrigger = false;
        }

    }

    void Move(int inputVal)
    {
        anim.SetTrigger("Walk");
        if (inputVal == 0) //forward
        {
            Vector3 point = Vector3.MoveTowards(transform.position, player.transform.position, walkingSpeed * Time.fixedDeltaTime);
            nav.SetDestination(point);

        }
        if (inputVal == 1) //left
        {
            nav.SetDestination(transform.position - transform.right);

        }
        if (inputVal == 2) //right
        {
            nav.SetDestination(transform.position + transform.right);

        }

    }


    //Information about the result of casting raycasts
    public struct RayCastInfo
    {
        public bool hit; //whether the raycast hit something
        public Vector3 point; //the endpoint of the ray
        public float distance; //length of the ray
        public float angle; //angle the ray was fired at

        public RayCastInfo(bool _hit, Vector3 _point, float _distance, float _angle)
        {
            hit = _hit;
            point = _point;
            distance = _distance;
            angle = _angle;
        }
    }

    //Deals with edges of obstacles in the mesh
    public struct EdgeInfo
    {
        //the edge will be between these two points
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}
