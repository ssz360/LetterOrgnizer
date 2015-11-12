namespace LetterOrganizer
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// the class contains database seeds in debugging mode
    /// </summary>
    public class ContextDebugInitializer : DropCreateDatabaseIfModelChanges<Context>
    {
        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void Seed(Context context)
        {
            var ringBinders = new List<RingBinder>()
            {
                new RingBinder(){ Title="مربوط به اموزش پرورش" , Comments="درون کشو دوم"},
                new RingBinder(){ Title="مربوط به اداره آب" , Comments="درون کشو دوم"},
                new RingBinder(){ Title="برق" , Comments="درون انباری"},
            };
            context.RingBinders.AddRange(ringBinders);
            context.SaveChanges();

            var letters = new List<Letter>
            {
                new Letter(){ LetterID= 55, RingerBinder=ringBinders[0], Owner="سجاد", Date=DateTime.Now, Attachment=true, Comment="توضیحات", Title="عنوان 1",},
                new Letter(){ LetterID= 585, RingerBinder=ringBinders[0], Owner="علی", Date=DateTime.Now, Attachment=false, Comment="توضیحات", Title="عنوان 2",},
                new Letter(){ LetterID= 235, RingerBinder=ringBinders[1], Owner="حسین", Date=DateTime.Now, Attachment=true, Comment="توضیحات", Title="عنوان 3",},
                new Letter(){ LetterID= 225483, RingerBinder=ringBinders[2], Owner="احمد", Date=DateTime.Now, Attachment=false, Comment="توضیحات", Title="عنوان 4",},
            };

            context.Letters.AddRange(letters);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
