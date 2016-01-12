using UnityEngine;
using UnityEngine.UI;

using System.Collections;

using catwins.flexui;

public class Event : MonoBehaviour {

	public GameObject Container;
	
	public UnityEngine.Object XML;
	
	public Button BtnShow;
	
	
	void Start () {
		BtnShow.onClick.AddListener (Show);
		Show ();
	}
	
	Document doc;
	private void Show()
	{
		if (XML == null) {
			Debug.LogError("XML not set");
			return;
		}
		
		if (doc != null) {
			doc.Dispose();
			doc = null;
		}
		
		string xmlString = XML.ToString ();
		doc = new Document (Container, xmlString);

		doc.AddEventListener ("onBtnClick", OnClickBtn);
		doc.AddEventListener ("onClickToggle", OnClickToggle);
	}

	private void OnClickToggle(object element, object data)
	{
		TagToggle toggle = (TagToggle)element;
		Debug.Log ("You clicked :" + toggle.ID);
	}

	private void OnClickBtn(object element, object data)
	{
		TagButton btn = (TagButton)element;
		Debug.Log ("You just clicked me! My name is: " + btn.Name + ", text on me is:" + btn.Text);
		var group = ModToggleGroup.GetGroupByGroupID (1);
		var selectedToggles = group.GetSelectedToggles ();

		for (int i = 0; i < selectedToggles.Count; i++) {
			Debug.Log ("You selected:" + selectedToggles [i].ID);
		}
		btn.Text += "~";
	}
}
