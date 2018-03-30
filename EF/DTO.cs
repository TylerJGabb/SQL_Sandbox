namespace ConsoleStagingSandbox
{
    class Program
    {
        
        static void Main(string[] args)
        {
            using (var db = new PolyPrintEntities())
            {
                var minAssoc = db.Associates
                    .Where(a => a.Is_Active)
                    .Select(a => new AssociateDto()
                    {
                        Record_Number = a.Record_Number, //reduces load on db
                        Name = a.Name,			//like writing (select col1,col2 where...)
                        Email = a.EMail    		//instead of (select *)

                    })
                    .ToList();

            }
        }
    }
}
