
using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace FlexUI
{
    [InterestedProperty(TOGGLE_GROUP_ID)]
    public class ModToggleGroup : BaseMod 
    {
        public const string TOGGLE_GROUP_ID = "togglegroupid";
        public const string ALLOW_SWITCH_OFF = "allowswitchoff";


        //-------
        private static Dictionary<int, ModToggleGroup> groups = new Dictionary<int, ModToggleGroup>();

        public static ModToggleGroup GetGroupByGroupID(int groupID)
        {
            if (groups.ContainsKey(groupID))
            {
                return groups[groupID];
            }
            return null;
        }

        private static Dictionary<int ,List<CtrlToggle>> togglesNeedGroup = new Dictionary<int, List<CtrlToggle>>();
        public static void RegGroupedToggle(int groupID, CtrlToggle ctrlToggle)
        {
            if (!togglesNeedGroup.ContainsKey(groupID))
            {
                togglesNeedGroup[groupID] = new List<CtrlToggle>();
            }
            togglesNeedGroup [groupID].Add(ctrlToggle);
        }


        //---------

        public ToggleGroup  ToggleGroup = null;
        public int GroupID = 0;

        protected override void OnAdd()
        {
            ToggleGroup = element.GameObject.AddComponent<ToggleGroup>();
            InitGroup();
        }

        protected override void OnRemove()
        {
            groups = new Dictionary<int, ModToggleGroup>();
            togglesNeedGroup = new Dictionary<int, List<CtrlToggle>>();
        }

        private void InitGroup()
        {
            if (element.HasDirtyProperty(TOGGLE_GROUP_ID))
            {
                GroupID = element.GetInt(TOGGLE_GROUP_ID);
            }
            if (groups.ContainsKey(GroupID) && groups [GroupID] != this)
            {
                Debug.LogWarning("FlexUI: ModToggleGroup: Group ID[" + GroupID + "]  conflict.");
            } else
            {
                groups[GroupID] = this;
            }

            if (togglesNeedGroup.ContainsKey(GroupID))
            {
                List<CtrlToggle> togglesInGroup = togglesNeedGroup[GroupID];
                for (int i = 0; i < togglesInGroup.Count; i++)
                {
                    togglesInGroup[i].Toggle.group = ToggleGroup;
                }
                togglesNeedGroup.Remove(GroupID);
            }
        }
        public override void Update()
        {
            InitGroup();

            if (element.HasDirtyProperty(ALLOW_SWITCH_OFF))
            {
                ToggleGroup.allowSwitchOff = element.GetBool(ALLOW_SWITCH_OFF);
            }
            
        }


    }
}

