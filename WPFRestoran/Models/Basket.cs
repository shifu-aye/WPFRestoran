namespace WPFRestoran.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public partial class Basket
    {
        public int Id { get; set; }
        public Nullable<int> FK_Menu { get; set; }
        public Nullable<int> FK_User { get; set; }
        public double Summ
        {
            get { return SummBasketCalculation(); }
            set { Summ = value; OnPropertyChanged("Summ"); }
        }

        public virtual Menu Menu { get; set; }
        public virtual User User { get; set; }
        public ObservableCollection<Menu> Products
        {
            get { return Products; }
            set { Products = value; OnPropertyChanged("Products"); }
        }

        private void OnPropertyChanged(string v)
        {
            throw new NotImplementedException();
        }
        public double SummBasketCalculation()
        {
            foreach(var item in Products)
            {
                Summ += item.Price;
            }
            return Summ;
        }
    }
}
