using PosTech.Fase3.AreaCode.API.Model;


namespace PosTech.Fase3.AreaCodeModel.API.Helpers
{
    public static class RegionHelper
    {
        public static List<RegionModel> Get()
        { 
            return regions;
        }
        private static List<RegionModel> regions = new List<RegionModel>
        {
            new RegionModel("Norte", new List<StateModel>
            {
                new StateModel("Acre", new List<CodeModel> { new CodeModel(68) }),
                new StateModel("Amapá", new List<CodeModel> { new CodeModel(96) }),
                new StateModel("Amazonas", new List<CodeModel> { new CodeModel(92), new CodeModel(97) }),
                new StateModel("Pará", new List<CodeModel> { new CodeModel(91), new CodeModel(93), new CodeModel(94) }),
                new StateModel("Rondônia", new List<CodeModel> { new CodeModel(69) }),
                new StateModel("Roraima", new List<CodeModel> { new CodeModel(95) }),
                new StateModel("Tocantins", new List<CodeModel> { new CodeModel(63) })
            }),

            new RegionModel("Nordeste", new List<StateModel>
            {
                new StateModel("Alagoas", new List<CodeModel> { new CodeModel(82) }),
                new StateModel("Bahia", new List<CodeModel> { new CodeModel(71), new CodeModel(73), new CodeModel(74), new CodeModel(75), new CodeModel(77) }),
                new StateModel("Ceará", new List<CodeModel> { new CodeModel(85), new CodeModel(88) }),
                new StateModel("Maranhão", new List<CodeModel> { new CodeModel(98), new CodeModel(99) }),
                new StateModel("Paraíba", new List<CodeModel> { new CodeModel(83) }),
                new StateModel("Pernambuco", new List<CodeModel> { new CodeModel(81), new CodeModel(87) }),
                new StateModel("Piauí", new List<CodeModel> { new CodeModel(86), new CodeModel(89) }),
                new StateModel("Rio Grande do Norte", new List<CodeModel> { new CodeModel(84) }),
                new StateModel("Sergipe", new List<CodeModel> { new CodeModel(79) })
            }),

            new RegionModel("Centro-Oeste", new List<StateModel>
            {
                new StateModel("Distrito Federal", new List<CodeModel> { new CodeModel(61) }),
                new StateModel("Goiás", new List<CodeModel> { new CodeModel(62), new CodeModel(64) }),
                new StateModel("Mato Grosso", new List<CodeModel> { new CodeModel(65), new CodeModel(66) }),
                new StateModel("Mato Grosso do Sul", new List<CodeModel> { new CodeModel(67) })
            }),

            new RegionModel("Sudeste", new List<StateModel>
            {
                new StateModel("Espírito Santo", new List<CodeModel> { new CodeModel(27), new CodeModel(28) }),
                new StateModel("Minas Gerais", new List<CodeModel> { new CodeModel(31), new CodeModel(32), new CodeModel(33), new CodeModel(34), new CodeModel(35), new CodeModel(37), new CodeModel(38) }),
                new StateModel("Rio de Janeiro", new List<CodeModel> { new CodeModel(21), new CodeModel(22), new CodeModel(24) }),
                new StateModel("São Paulo", new List<CodeModel> { new CodeModel(11), new CodeModel(12), new CodeModel(13), new CodeModel(14), new CodeModel(15), new CodeModel(16), new CodeModel(17), new CodeModel(18), new CodeModel(19) })
            }),

            new RegionModel("Sul", new List<StateModel>
            {
                new StateModel("Paraná", new List<CodeModel> { new CodeModel(41), new CodeModel(42), new CodeModel(43), new CodeModel(44), new CodeModel(45), new CodeModel(46) }),
                new StateModel("Rio Grande do Sul", new List<CodeModel> { new CodeModel(51), new CodeModel(53), new CodeModel(54), new CodeModel(55) }),
                new StateModel("Santa Catarina", new List<CodeModel> { new CodeModel(47), new CodeModel(48), new CodeModel(49) })
            })
        };
    }
}
