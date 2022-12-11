using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.DiningRoom.Actors;
using AppRestaurant.Controller.DiningRoom.Observer;
namespace AppRestaurant.Model.DiningRoom.Factory
{
    internal class CustomersFactory : AbstractCustomersFactory, IObservable<CustomerGroup>
    {
        private List<IObserver<CustomerGroup>> observers;
        public CustomersFactory()
        {
            observers = new List<IObserver<CustomerGroup>>();
        }
        public override CustomerGroup CreateCustomers(int nbCustomers)
        {
            CustomerGroup customer_ = new CustomerGroup(nbCustomers);

            foreach (var observer in observers)
                observer.OnNext(customer_);

            return customer_;
        }

        public IDisposable Subscribe(IObserver<CustomerGroup> observer)
        {
            // Check whether observer is already registered. If not, add it
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
                // Provide observer with existing data.
                //foreach (var item in flights)
                //    observer.OnNext(item);
            }
            return new Unsubscriber<CustomerGroup>(observers, observer);
        }

    }
}
