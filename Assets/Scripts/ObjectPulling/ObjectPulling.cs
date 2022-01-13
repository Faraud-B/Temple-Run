using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OP
{
    public class ObjectPulling
    {
        private string rootParentName = "ObjectPulling";

        private GameObject masterGO;
        private int initNumber;
        private GameObject parent;

        List<GameObject> listGO;

        public ObjectPulling(GameObject go, int iNum, string parentName)
        {
            this.masterGO = go;
            this.initNumber = iNum;

            this.parent = new GameObject(parentName);

            this.parent.transform.parent = CreateDirectory().transform;

            listGO = new List<GameObject>();

            for (int i = 0; i < initNumber; i++)
            {
                listGO.Add(CreateGO());
            }
        }

        public GameObject GetObject()
        {
            if (listGO.Count == 0)
                return CreateGO();

            else
            {
                GameObject temp = listGO[0];
                listGO.RemoveAt(0);
                return temp;
            }
        }

        private GameObject CreateGO()
        {
            GameObject temp = GameObject.Instantiate(masterGO) as GameObject;
            temp.SetActive(false);
            temp.transform.parent = parent.transform;

            temp.AddComponent<ObjectPullingChild>();
            temp.GetComponent<ObjectPullingChild>().SetMaster(this);

            return temp;
        }

        public void AddToList(GameObject go)
        {
            if(!listGO.Contains(go))
            {
                listGO.Add(go);
            }
        }

        public GameObject CreateDirectory()
        {
            GameObject root = GameObject.Find("/" + rootParentName);
            if(root == null)
            {
                root = new GameObject(rootParentName);
            }
            return root;
        }
    }
}
