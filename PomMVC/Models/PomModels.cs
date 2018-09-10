using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.ComponentModel;

namespace PomMVC.Models
{
    public class Project
    {
        [Key]
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "Please enter project name")]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        public virtual ICollection<Transaction> Transaction { get; set; }
    }

    public class Vers
    {
        [Key]
        public int VersID { get; set; }

        [Required(ErrorMessage = "Please enter version no")]
        [Display(Name = "Version No")]
        public string VersNo { get; set; }

        public virtual ICollection<Transaction> Transaction { get; set; }

    }

    public class Page
    {
        [Key]
        public int PageID { get; set; }

        [Required(ErrorMessage = "Please enter page name")]
        [Display(Name = "Page Name")]
        public string PageName { get; set; }

        public virtual ICollection<Transaction> Transaction { get; set; }
    }

    public class Transaction
    {
        [Key]
        public int TransID { get; set; }
        public string UserId { get; set; }
        public int ProjectID { get; set; }
        public int VersID { get; set; }
        public int PageID { get; set; }

        [Display(Name = "Transaction Type")]
        public TransType TransType { get; set; }

        private DateTime _entryDate = DateTime.Now;

        [Display(Name = "Transaction Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]
        public DateTime TransDate
        {
            get
            {
                return _entryDate;
            }
            set
            {
                _entryDate = value;
            }
        }

        [Display(Name = "Place")]
        public EditPlace TransFunEle { get; set; }

        [Display(Name = "Changed Code")]
        [DataType(DataType.MultilineText)]
        public string TransText { get; set; }

        [ForeignKey("ProjectID")]
        public virtual Project Project { get; set; }
        [ForeignKey("VersID")]
        public virtual Vers Vers { get; set; }

        [ForeignKey("PageID")]
        public virtual Page Page { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }

    public enum EditPlace
    {
        Function,
        Element
    }

    public enum TransType
    {
        New,
        Update,
        Delete
    }
}