using AuthServer.Exceptions;

namespace AuthServer.Core.Domain.Errors;


public static class RoleErrors
{
    public static readonly Error AlreadyExists = new Error("role.alreadyExists", "Роль уже существует");
    
    public static readonly Error NotFound = new Error("role.notFound", "Роль не найдена");

}