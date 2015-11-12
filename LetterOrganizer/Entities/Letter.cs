namespace LetterOrganizer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// The letter class
    /// </summary>
    public class Letter : Database
    {
        #region fields
        /// <summary>
        /// The letter identifier
        /// </summary>
        private int letterID;

        /// <summary>
        /// The received letter identifier
        /// </summary>
        private int receivedLetterId;

        /// <summary>
        /// The letter's comment
        /// </summary>
        private string comment;

        /// <summary>
        /// The letter's title
        /// </summary>
        private string title;

        /// <summary>
        /// The letter's owner
        /// </summary>
        private string owner;

        /// <summary>
        /// The letter's attachment
        /// </summary>
        private bool attachment;

        /// <summary>
        /// The letter's date
        /// </summary>
        private DateTime date;

        /// <summary>
        /// The letter's ringer binder
        /// </summary>
        private RingBinder ringerBinder;
        #endregion

        #region properties
        /// <summary>
        /// Gets or sets the letter's ringer binder.
        /// </summary>
        /// <value>
        /// The letter's ringer binder.
        /// </value>
        [Required(ErrorMessage = "نامه باید دارای زوکن باشد.")]
        public RingBinder RingerBinder
        {
            get
            {
                return this.ringerBinder;
            }

            set
            {
                this.ringerBinder = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Letter"/> has attachment.
        /// </summary>
        /// <value>
        ///   <c>true</c> if attachment; otherwise, <c>false</c>.
        /// </value>
        public bool Attachment
        {
            get
            {
                return this.attachment;
            }

            set
            {
                this.attachment = value;
            }
        }

        /// <summary>
        /// Gets or sets the letter's owner.
        /// </summary>
        /// <value>
        /// The letter's owner.
        /// </value>
        [Required(ErrorMessage = "صاحب نامه باید مشخص باشد.")]
        public string Owner
        {
            get
            {
                return this.owner;
            }

            set
            {
                this.owner = value;
            }
        }

        /// <summary>
        /// Gets or sets the received letter's identifier.
        /// </summary>
        /// <value>
        /// The received letter's identifier.
        /// </value>
        public int ReceivedLetterId
        {
            get
            {
                return this.receivedLetterId;
            }

            set
            {
                this.receivedLetterId = value;
            }
        }

        /// <summary>
        /// Gets or sets the letter identifier.
        /// </summary>
        /// <value>
        /// The letter identifier.
        /// </value>
        [Required(ErrorMessage = "نامه باید دارای شماره باشد.")]
        public int LetterID
        {
            get
            {
                return this.letterID;
            }

            set
            {
                this.letterID = value;
            }
        }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment
        {
            get
            {
                return this.comment;
            }

            set
            {
                this.comment = value;
            }
        }

        /// <summary>
        /// Gets or sets the title of letter.
        /// </summary>
        /// <value>
        /// The title of letter.
        /// </value>
        [Required(ErrorMessage = "نامه باشد عنوان داشته باشد.")]
        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
            }
        }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date
        {
            get
            {
                return this.date;
            }

            set
            {
                this.date = value;
            }
        }
        #endregion
    }
}
