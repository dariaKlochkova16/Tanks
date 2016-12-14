using System.Xml.Serialization;
using System.IO;
using System.Drawing;
namespace DKFramework
{
    public static class LevelFile
    {
        public static void Open(string fileName)
        {
            SaveObject[] saveObjects;
            XmlSerializer formatter = new XmlSerializer(typeof(SaveObject[]));
            TextReader reader = new StreamReader(fileName);
            saveObjects = (SaveObject[])formatter.Deserialize(reader);

            foreach (SaveObject element in saveObjects)
            {
                GameObject elementGame = GameObjectFactory.CreateGameObject(ObjectTypeTools.Convert(element.name));
                elementGame.GetComponent<Transform>().X = element.point.X;
                elementGame.GetComponent<Transform>().Y = element.point.Y;
                elementGame.GetComponent<Collider>().Add();
              
                Core.Instance.Add(elementGame);
            }
        }

        public static void Save(string fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SaveObject[]));
            SaveObject[] saveObjects = new SaveObject[Core.Instance.Count];

            for (int i = 0; i < Core.Instance.Count; i++)
            {
                saveObjects[i] = new SaveObject();
                saveObjects[i].name = Core.Instance.GetElenent(i).Name;
                saveObjects[i].point = new PointF(Core.Instance.GetElenent(i).GetComponent<Transform>().X, Core.Instance.GetElenent(i).GetComponent<Transform>().Y);
            }

            TextWriter writer = new StreamWriter(fileName);
            xmlSerializer.Serialize(writer, saveObjects);
            writer.Close();

        }
    }
}
