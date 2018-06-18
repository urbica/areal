using System.Xml.Serialization;
using System.IO;

public static class SaveHelper {
	public static string Serialize<T>(this T toSerialize){
		XmlSerializer xml = new XmlSerializer(typeof(T));
		StringWriter writer = new StringWriter();
		xml.Serialize(writer,toSerialize);
		return writer.ToString();
	}

	public static T Deserialize<T>(this string toDeserialize){
		XmlSerializer xml = new XmlSerializer(typeof(T));
		StringReader reader = new StringReader(toDeserialize);
		return (T)xml.Deserialize(reader);
	} 
}
