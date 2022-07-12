using System.Threading.Tasks;

namespace CSPlayground.controllers;

public interface IProgramController
{
    Task Run(string path);
}