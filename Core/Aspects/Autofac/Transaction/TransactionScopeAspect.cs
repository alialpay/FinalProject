using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {
        // yapılmak istenen işlemin ileriki basamaklarında bir problem olursa bütün işlemi geri almayı sağlar. örn para aktarımı
        public override void Intercept(IInvocation invocation)      // invocation bizim metotumuz       // intercept demek bu metot yerine bu şablon bloğu çalıştır demek
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();               // Proceed demek : bunu çalıştır // yani burada metodu çalıştır diyoruz
                    transactionScope.Complete();
                }
                catch (System.Exception e)
                {
                    transactionScope.Dispose();         // exception olduğunda dispose et
                    throw;
                }
            }
        }
    }
}
