using System;

namespace Shared.Application.Exceptions;

public class BusinessException(string message)  : Exception(message)
{

}
