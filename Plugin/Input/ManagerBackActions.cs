using System;
using System.Collections.Generic;
using UnityEngine;

namespace LUT
{

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

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Act();
			}
		}

		public void RemoveStack()
		{
			if (stack == null)
			{
				RestartStack();
			}

			if (stack.Count == 0)
			{
				return;
			}

			stack.Pop();
		}
		public void AddStack(Action action)
		{
			if (stack == null)
			{
				RestartStack();
			}

			stack.Push(action);
		}

		public void Act()
		{
			if (stack == null)
			{
				stack = new Stack<Action>();
			}

			if (stack.Count == 0)
			{
				if (endAction != null)
				{
					endAction();
				}
				else
				{
					Application.Quit();
				}

				return;
			}


			Action action = stack.Pop();
			action();

		}

		public void RestartStack()
		{
			if (stack == null)
			{
				stack = new Stack<Action>();
			}
			else
			{
				stack.Clear();
			}
		}
	}
}
