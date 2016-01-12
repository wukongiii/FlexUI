
using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace catwins.flexui
{
    [InterestedProperty(TOGGLE_GROUP_ID)]
    public class ModToggleGroup : BaseMod 
    {
        public const string TOGGLE_GROUP_ID = "togglegroupid";
        public const string ALLOW_SWITCH_OFF = "allowswitchoff";

		//I think Unity should leave ToggleGroup invisible, seperated Toggle and ToggleGroup make things weird.
        //-------static------
        private static Dictionary<int, ModToggleGroup> groups = new Dictionary<int, ModToggleGroup>();

        public static ModToggleGroup GetGroupByGroupID(int groupID)
        {
            if (groups.ContainsKey(groupID))
            {
                return groups[groupID];
            }
            return null;
        }

		private static Dictionary<int ,List<CtrlToggle>> groupedToggles = new Dictionary<int, List<CtrlToggle>>();
        public static void RegGroupedToggle(int groupID, CtrlToggle ctrlToggle)
        {
            if (!groupedToggles.ContainsKey(groupID))
            {
                groupedToggles[groupID] = new List<CtrlToggle>();
            }
            groupedToggles [groupID].Add(ctrlToggle);
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
            groupedToggles = new Dictionary<int, List<CtrlToggle>>();
			//doing this save a lot of code.
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

            if (groupedToggles.ContainsKey(GroupID))
            {
                List<CtrlToggle> togglesInGroup = groupedToggles[GroupID];
                for (int i = 0; i < togglesInGroup.Count; i++)
                {
                    togglesInGroup[i].Toggle.group = ToggleGroup;
                }
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

		public List<TagToggle> GetSelectedToggles()
		{
			if (!groupedToggles.ContainsKey (GroupID))
			{
				return null;
			}

			List<TagToggle> selectedToggles = new List<TagToggle> ();
			var toggles = groupedToggles [GroupID];
			for (int i = 0; i < toggles.Count; i++) 
			{
				if (toggles [i].Toggle.isOn) 
				{
					selectedToggles.Add (toggles [i].TagToggle);
				}
			}
			return selectedToggles;
		}


    }
}

