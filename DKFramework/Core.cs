using System.Collections.Generic;
using SharpGL;
using System.Linq;
using System;

namespace DKFramework
{
    public class Core
    {
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
        }

        public void Clear()
        {
            _components.Clear();
        }

        public int Count
        {
            get { return _elements.Count; }
        }

        public GameObject GetElenent(int i)
        {
            return _elements[i];
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
