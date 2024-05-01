using DesignPatterns.Builder;
using DesignPatterns.Prototype;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace DesignPatterns
{
    internal static class Extensions
    {
        public static FunctionalBuilder.PersonBuilder? WorksAs
            (this FunctionalBuilder.PersonBuilder builder, string position)
            => builder.Do(p => p.Position = position);

        public static T DeepCopy<T>(this PrototypeInheritance.IDeepCopyable<T> item)
            where T : new()
        {
            return item.DeepCopy();
        }

        public static T DeepCopy<T> (this T person)
            where T : PrototypeInheritance.Person, new()
        {
            return ((PrototypeInheritance.IDeepCopyable<T>)person).DeepCopy();
        }

        public static T? DeepCopyXml<T>(this T self)
        {
            using (var stream = new MemoryStream())
            {
                XmlSerializer serializer = new(typeof(T));
                serializer.Serialize(stream, self);

                stream.Seek(0, SeekOrigin.Begin);
                object? copy = serializer.Deserialize(stream);
                stream.Close();

                return (T?)copy;
            }
        }
    }
}
