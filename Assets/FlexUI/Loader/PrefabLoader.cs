
using UnityEngine;

namespace FlexUI
{

    public class PrefabLoader
    {

        static public GameObject Load(string path)
        {
            var rawRes = Resources.Load(path);
            if (rawRes == null)
            {
                return null;
            }
            return (GameObject)GameObject.Instantiate(rawRes);
        }
    }

}