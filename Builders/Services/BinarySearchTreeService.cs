using Builders.Interfaces;

namespace Builders.Services
{
    public class BinarySearchTreeService
    {
        private readonly IBinarySearchTreeRepository repository;

        public BinarySearchTreeService(IBinarySearchTreeRepository repository)
        {
            this.repository = repository;
        }

        
    }
}