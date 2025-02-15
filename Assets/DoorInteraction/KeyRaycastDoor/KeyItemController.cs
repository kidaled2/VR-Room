using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KeySystem
{
    public class KeyItemController : MonoBehaviour
    {
        [Header("Key Types")]
        [SerializeField] public bool redDoor = false;
        [SerializeField] public bool redKey = false;
        [SerializeField] public bool blueDoor = false;
        [SerializeField] public bool blueKey = false;
        [SerializeField] public bool yellowDoor = false;
        [SerializeField] public bool yellowKey = false;

        [SerializeField] private KeyInventory _keyInventory = null;
        private KeyDoorController doorObject;

        private void Start()
        {
            if (redDoor || blueDoor || yellowDoor)
            {
                doorObject = GetComponent<KeyDoorController>();
            }
        }

        public void ObjectInteraction()
        {
            if (redDoor || blueDoor || yellowDoor)
            {
                doorObject.PlayAnimation();
            }
            else if (redKey)
            {
                _keyInventory.hasRedKey = true;
                gameObject.SetActive(false);
            }
            else if (blueKey)
            {
                _keyInventory.hasBlueKey = true;
                gameObject.SetActive(false);
            }
            else if (yellowKey)
            {
                _keyInventory.hasYellowKey = true;
                gameObject.SetActive(false);
            }
        }
    }
}
