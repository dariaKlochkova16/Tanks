using System.Collections.Generic;
using SharpGL;
using System.Linq;
using System;

namespace DKFramework
{
    public class Core
    {
        public event EventHandler DiedGameObject;

        private static Core _instance;
        public static Core Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Core();
                return _instance;
            }
        }

        private Core()
        {
        }

        private readonly List<GameObject> _elements = new List<GameObject>();

        private readonly List<CoreComponentBase> _components = new List<CoreComponentBase>();

        public OpenGL GLControl
        {
            get;
            set;
        }

        public void Add(GameObject element)
        {
            _elements.Add(element);
        }

        public void Draw()
        {
            foreach (CoreComponentBase el in _components)
            {
                if (GLControl != null)
                    el.OnDraw(GLControl);
            }
        }

        public void Remove(GameObject element)
        {
            _elements.Remove(element);
            if (DiedGameObject != null)
                DiedGameObject(element, new EventArgs());
        }

        public void Clear()
        {
            _components.Clear();
        }

        public int Count
        {
            get { return _elements.Count; }
        }

        public GameObject GetElement(int i)
        {
            return _elements[i];
        }

        public GameObject GetElement(int x, int y)
        {
            foreach(GameObject el in _elements)
            {
                Transform transform = el.GetComponent<Transform>();
                if (transform.X == x && transform.Y == y)
                    return el;
            }
            return null;
        }

        public T GetComponent<T>()
        {
            return _components.OfType<T>().FirstOrDefault();
        }

        public void AddComponent<T>() where T : CoreComponentBase
        {
            _components.Add((CoreComponentBase)Activator.CreateInstance(typeof(T), this));
        }

        public void Update(float deltaTime)
        {
            foreach (CoreComponentBase el in _components)
            {
                el.Update(deltaTime);
            }

            Draw();
        }


        public void ReCreate()
        {
            _instance = new Core();
        }
    }
}
