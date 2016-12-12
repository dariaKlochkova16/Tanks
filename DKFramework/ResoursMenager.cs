using System.IO;
using System.Xml.Serialization;

namespace DKFramework
{
    public class ResoursMenager : CoreComponentBase
    {
       private  TextureDescriptor[] _textureCard;

        public  TextureDescriptor[] TextureCard
        {
            get
            {
                return _textureCard;        
            }
        }

        private void Create()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(TextureDescriptor[]));
            TextReader reader = new StreamReader("TextureDescriptor.xml");
            _textureCard = (TextureDescriptor[])formatter.Deserialize(reader);
        }

        public ResoursMenager(Core core) : base(core)
        {
            Create();
        }
    }
}
