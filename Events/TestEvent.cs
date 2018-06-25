using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LUT.Event
{
    public class TestEvent : MonoBehaviour
    {
        public EventObject targetEvent;

        [EasyButtons.Button]
        public void Invoke()
        {
            targetEvent.Invoke();
        }
    }
}
