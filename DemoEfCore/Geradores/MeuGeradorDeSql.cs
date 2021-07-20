using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Linq.Expressions;

namespace DemoEfCore.Geradores
{
    public class MeuGeradorDeSql : SqlServerQuerySqlGenerator
    {
        public MeuGeradorDeSql(QuerySqlGeneratorDependencies dependencies) : base(dependencies)
        {

        }

        protected override Expression VisitTable(TableExpression tableExpression)
        {
            var table = base.VisitTable(tableExpression);
            Sql.Append(" WITH (NOLOCK)");

            return table;
        }
    }

    public class MeuGeradorDeSqlFactory : SqlServerQuerySqlGeneratorFactory
    {

        private readonly QuerySqlGeneratorDependencies _dependencies;

        public MeuGeradorDeSqlFactory(QuerySqlGeneratorDependencies dependencies) : base(dependencies)
        {
            _dependencies = dependencies;
        }

        public override QuerySqlGenerator Create()
        {
            return new MeuGeradorDeSql(_dependencies);
        }
    }
}
