using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OP
{
    public class ObjectPullingChild : MonoBehaviour
    {
        ObjectPulling master;

        public void SetMaster(ObjectPulling op)
        {
            master = op;
        }

        private void OnDisable()
        {
            master.AddToList(this.gameObject);
        }
    }
}
