using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LUT.Event
{
    public class TestEvent : MonoBehaviour
    {
        public Event targetEvent;

        [EasyButtons.Button]
        public void Invoke()
        {
            targetEvent.Invoke();
        }
    }
}
