namespace Common.DbHelper;

public abstract class DbConnection<T>
{
   public abstract T DbConnect();
}