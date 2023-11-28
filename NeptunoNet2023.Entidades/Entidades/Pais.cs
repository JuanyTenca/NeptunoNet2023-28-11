namespace NeptunoNet2023.Entidades.Entidades
{
	public class Pais
	{
		public int PaisId { get; set; }
        public string? NombrePais { get; set; }
        public byte[] RowVersion { get; set; }

		public override string ToString()
		{
			return $"{NombrePais}";
		}
	}
}
