
namespace FlexUI
{
    [InterestedProperty(NAME, ID)]
    public class ModNameID:BaseMod
    {
        public const string NAME = "name";
        public const string ID = "id";

        public override void Update()
        {
            string tagName = element.GetString(Tag.TAG);
            if (element.HasProperty(ID))
            {
                string id = element.GetString(ID, true);
                if (!string.IsNullOrEmpty(id))
                {
                    tagName = tagName + "-" + id;
                    element.document.RegisterElement(id, element);
                }
            }
            element.GameObject.name = tagName;

        }
    }

}
