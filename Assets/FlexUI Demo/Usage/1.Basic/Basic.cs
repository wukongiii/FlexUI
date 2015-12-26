using UnityEngine;
using UnityEngine.UI;

using System.Collections;

using catwins.flexui;

public class Basic : MonoBehaviour {

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
	}
}
