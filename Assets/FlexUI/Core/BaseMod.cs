
namespace catwins.flexui
{
    public class BaseMod
    {
        protected Element element;

        private void Add(Element element)
        {
            this.element = element;
            OnAdd();
        }

        private void Remove()
        {
            OnRemove();
            element = null;
        }

        protected virtual void OnAdd()
        {

        }

        protected virtual void OnRemove()
        {

        }

        public void Dispose()
        {
            OnRemove();
        }

        public virtual void Update()
        {

        }

    }

}