using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Hold a stack of actions. Can be used for managing actions that the player/user can undo.
/// Developed by Lawendt. 
/// Available @ https://github.com/Lawendt/UnityLawUtilities
/// </summary>
public class ManagerBackActions : Singleton<ManagerBackActions>
{
    public GameObject WarningCloseApp;

    Stack<Action> stack;
    public Action endAction;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            act();
        }
    }

    public void removeStack()
    {
        if (stack == null)
            restartStack();

        if (stack.Count == 0)
            return;

        stack.Pop();
    }
    public void addStack(Action action)
    {
        if (stack == null)
            restartStack();

        stack.Push(action);
    }

    public void act()
    {
        if (stack == null)
            stack = new Stack<Action>();

        if (stack.Count == 0)
        {
            if (endAction != null)
                endAction();
            else
                Application.Quit();
            return;
        }


        Action action = stack.Pop();
        action();

    }

    public void restartStack()
    {
        if (stack == null)
            stack = new Stack<Action>();
        else
            stack.Clear();
    }
}
