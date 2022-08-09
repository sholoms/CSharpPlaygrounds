using System.Threading.Tasks;

namespace CSPlayground.services;

public interface IApiCalculator
{
    Task<string> Calculate(string input);
    Task<string> FileResults();
}
