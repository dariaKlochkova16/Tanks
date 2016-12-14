using System;
using System.Collections.Generic;
using System.Linq;

namespace DKFramework
{
    public class GameObject
    {
        public string Name { get; set; }

        private readonly List<ComponentBase> _listComponents = new List<ComponentBase>();

        public event EventHandler<MessageBase> MessageReceived;

        public GameObject(string name)
        {
            Name = name;
        }

        public void AddComponent<T>() where T : ComponentBase
        {
            _listComponents.Add((ComponentBase)Activator.CreateInstance(typeof(T), this));
        }

        public T GetComponent<T>()
        {
            return _listComponents.OfType<T>().FirstOrDefault();
        }

        // TODO HACK
        Render _renderer;

        public Render renderer
        {
            get
            {
                if (_renderer == null)
                    _renderer = GetComponent<Render>();

                return _renderer;
            }
        }

        public void SendMessage(MessageBase message)
        {
            MessageReceived(this, message);
        }

    }
}
