namespace TaxInvoicesWebApi.AggregatesModel.SeedWork;

public interface IFinder<T, Key> where T : IDto where Key : IComparable
{
}

public interface IDto
{

}