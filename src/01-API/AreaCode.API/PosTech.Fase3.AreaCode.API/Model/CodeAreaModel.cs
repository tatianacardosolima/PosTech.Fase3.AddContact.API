namespace PosTech.Fase3.AreaCode.API.Model
{
    public class RegionModel
    {
        public string Name { get; set; }
        public List<StateModel> States { get; set; }

        public RegionModel(string name, List<StateModel> states)
        {
            Name = name;
            States = states;
        }
    }

    public class StateModel
    {
        public string Name { get; set; }
        public List<CodeModel> Codes { get; set; }

        public StateModel(string name, List<CodeModel> codes)
        {
            Name = name;
            Codes = codes;
        }
    }

    public class CodeModel
    {
        public int Number { get; set; }

        public CodeModel(int number)
        {
            Number = number;
        }
    }

    public class RegionStateCodeModel
    {
        public int Number { get; set; }

        public string State { get; set; } = string.Empty;

        public string Region { get; set; }= string.Empty;

    }
}
