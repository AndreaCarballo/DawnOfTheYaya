using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class QLearning
{
    private List<float[]> QStates;
    private List<float[]> QActions;

    private int numberPossibleActions; //4: up, down, right, left

    private float[] pastState; //vector that holds the previous state
    private int pastActionIndex; //index of optimal action (0, 1, 2 or 3)
    private float[] newState; //vector that holds the current state
    private float newActionValue; //value of action that will be executed

    private float learningRate = 0.8f;
    private float discountFactor = 0.95f;
    private float e = 1; // Initial epsilon value for random action selection.
    private float eMin = 0.1f; // Lower limit for epsilon.
    private int eSteps = 2000; // Number of steps to lower e to eMin.

    private bool firstIteration;
    System.Random random = new System.Random();

    public QLearning(int possActs)
    {
        QStates = new List<float[]>();
        QActions = new List<float[]>();
        numberPossibleActions = possActs;

        firstIteration = true;
    }

    public int getAction(float[] currentState, float reward)
    {
        iterationUpdate(currentState, reward); //updates the action quality


        pastState = currentState; //saves the current state as the previous one

        firstIteration = false;

        int actionIndex = random.Next(0, numberPossibleActions); //if it's the first iteration the action is randomly generated

        bool exists = searchState(currentState, false); //check if current state is stored
        if (exists)
        {
            //policy e-greedy
            if (Random.Range(0f, 1f) < e)
            {
                pastActionIndex = actionIndex; //we use the randomly generated action
            }
            if (e > eMin)
            {
                e = e - ((1f - eMin) / (float)eSteps);
            }
            return pastActionIndex; //returns the action the agent will take

        } else //if current state is not stored
        {
            float[] actionVals = new float[numberPossibleActions];
            for (int i = 0; i < numberPossibleActions; i++)
            {
                actionVals[i] = 0f; //actions associated with this state have blank values
            }
            QStates.Add(pastState);
            QActions.Add(actionVals);
        }

        pastActionIndex = actionIndex; //we use the randomly generated action
        return pastActionIndex;
    }

    public void iterationUpdate(float[] nState, float reward)
    {
        if (!firstIteration) //only if it's not the first iteration
        {
            newState = nState;

            bool exists = searchState(newState, true); //checks if the new state is stored

            for (int i = 0; i < QStates.Count; i++)
            {
                float[] state = QStates.ElementAt(i);
                float[] actions = QActions.ElementAt(i);

                if (state[0] == pastState[0] && state[1] == pastState[1]) //we update the quality of the past action taken for the previous state
                {

                    if (exists) //if the new state is already stored we take into account its optimal action value
                    {
                        actions[pastActionIndex] = (actions[pastActionIndex] + learningRate * (reward + discountFactor * newActionValue - actions[pastActionIndex]));
                    }

                    if (!exists) //if it isn't, we don't
                    {
                        actions[pastActionIndex] = (actions[pastActionIndex] + learningRate * (reward - actions[pastActionIndex]));
                    }
                }
            }
        }
    }

    public bool searchState(float[] stateToFind, bool update)
    {
        bool exists = false;
        if (QStates.Count > 0) //checks if q states has been initialized
        {

            for (int i = 0; i < QStates.Count; i++)
            {
                // saves every state vector with its corresponding action vector
                float[] state = QStates.ElementAt(i);
                float[] actions = QActions.ElementAt(i);

                if (state[0] == stateToFind[0] && state[1] == stateToFind[1])
                {
                    exists = true; //marks that the state is already stored

                    if (!update) //getting optimal action to send to agent
                    {
                        pastActionIndex = Array.IndexOf(actions, actions.Max()); //saves index of optimal action
                       //Debug.Log("Picked action value:          " + actions[pastActionIndex]); //prints the value associated with the action
                        
                    } else //getting quality of action for the formula
                    {
                        newActionValue = actions.Max(); //saves value of optimal action
                    }
                    return exists;
                }
            }
        }
        return exists;
    }
}

