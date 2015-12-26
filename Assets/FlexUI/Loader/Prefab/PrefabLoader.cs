
using UnityEngine;

namespace catwins.flexui
{

    public class PrefabLoader
    {

        static public GameObject Load(string path)
        {
            var rawRes = Resources.Load<GameObject>(path);
            if (rawRes == null)
            {
                return null;
            }
            return GameObject.Instantiate<GameObject>(rawRes);
        }
    }

}