namespace CharacterBuilder.Core.Interfaces
{
    interface ISpecification<T>
    {
        bool IsSatisfiedBy(T obj);
    }
}
