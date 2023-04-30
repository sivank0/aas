namespace PMS.Tools.Types;

public class DataUrl
{
	public static DataUrl? Parse(String? value)
	{
		if (String.IsNullOrEmpty(value) || !value.StartsWith("data:", StringComparison.OrdinalIgnoreCase)) return null;

		const Int32 prefixSize = 5;
		Int32 dataIndex = value.IndexOf(',', prefixSize);
		if (dataIndex < 0) return null;

		String data = value.Substring(dataIndex + 1);
		String type = value.Substring(prefixSize, dataIndex - prefixSize);
		if (!type.EndsWith("base64", StringComparison.OrdinalIgnoreCase)) return null;

		return new(type, data);
	}

	public String Type { get; }
	public String Data { get; }
	public Int32 ByteCount
	{
		get
		{
			if (String.IsNullOrEmpty(Data)) return 0;

			Int32 characterCount = Data.Length;
			Int32 paddingCount = Data.Substring(characterCount - 2, 2).Count(c => c == '=');

			return (3 * (characterCount / 4)) - paddingCount;
		}
	}
	public String Extension
	{
		get
		{
			if (Type.StartsWith("application/pdf")) return ".pdf";
			if (Type.StartsWith("image/png")) return ".png";
			if (Type.StartsWith("image/jpeg")) return ".jpg";
			if (Type.StartsWith("image/jpg")) return ".jpg";
			if (Type.StartsWith("application/vnd.openxmlformats-officedocument.wordprocessingml.document")) return ".docx";
			if (Type.StartsWith("application/msword")) return ".doc";
			if (Type.StartsWith("application/vnd.ms-word.document.macroEnabled.12")) return ".docm";
			if (Type.StartsWith("application/msword")) return ".dot";
			if (Type.StartsWith("application/vnd.ms-word.template.macroEnabled.12")) return ".dotm";
			if (Type.StartsWith("application/vnd.ms-excel")) return ".xls";
			if (Type.StartsWith("application/vnd.ms-excel.sheet.binary.macroEnabled.12")) return ".xlsb";
			if (Type.StartsWith("application/vnd.ms-excel.sheet.macroEnabled.12")) return ".xlsm";
			if (Type.StartsWith("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")) return ".xlsx";
			if (Type.StartsWith("application/vnd.ms-powerpoint")) return ".ppt";
			if (Type.StartsWith("application/xml")) return ".xml";
			if (Type.StartsWith("text/plain")) return ".txt";
			if (Type.StartsWith("text/xml")) return ".xml";
			if (Type.StartsWith("video/mp4")) return ".mp4";
			if (Type.StartsWith("video/mpeg")) return ".mpg";
			if (Type.StartsWith("video/quicktime")) return ".mov";

			return "";
		}
	}

	public DataUrl(String type, String data)
	{
		Type = type;
		Data = data;
	}

	public Byte[] ToByteArray()
	{
		return Convert.FromBase64String(Data);
	}
}