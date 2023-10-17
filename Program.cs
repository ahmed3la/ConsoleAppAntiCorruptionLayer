namespace ConsoleAppAntiCorruptionLayer
{
    public class Program
    {

        #region FirstExample

        //static void Main(string[] args)
        //{
        //    // Initialize the Anti-Corruption Layer
        //    IAntiCorruptionLayer acl = new AntiCorruptionLayer();

        //    // Create instances of ModuleA and ModuleB
        //    ModuleA moduleA = new ModuleA();
        //    ModuleB moduleB = new ModuleB();

        //    // ModuleA uses the Anti-Corruption Layer to get data from ModuleB
        //    string result = moduleA.GetDataFromModuleB(acl);

        //    // Display the result
        //    Console.WriteLine(result);
        //}




        #endregion




        #region BillingExample


        static void Main()
        {
            // Create instances of BillingModule and AccountingModule (or use dependency injection)
            IBillingService billingService = new BillingModule();
            IAccountingService accountingService = new AccountingModule();

            // Create the Anti-Corruption Layer with a reference to the AccountingModule
            var antiCorruptionLayer = new AntiCorruptionLayer(accountingService);

            // Process an invoice through the Anti-Corruption Layer
            InvoiceData invoice = new InvoiceData
            {
                InvoiceNumber = 12345,
                Amount = 100.00m
            };

            antiCorruptionLayer.ProcessInvoice(invoice);
        }


        #endregion
    }










    //#region FirstExample


    //// ModuleA
    //public class ModuleA
    //{
    //    public string GetDataFromModuleB(IAntiCorruptionLayer acl)
    //    {
    //        // ModuleA communicates with ModuleB through the ACL
    //        string dataFromModuleB = acl.GetDataFromModuleB();
    //        return "Data from ModuleB (ModuleA): " + dataFromModuleB;
    //    }
    //}

    //// ModuleB
    //public class ModuleB
    //{
    //    public string GetData()
    //    {
    //        // ModuleB provides its data
    //        return "Data from ModuleB";
    //    }
    //}

    //// Anti-Corruption Layer (ACL)
    //public interface IAntiCorruptionLayer
    //{
    //    string GetDataFromModuleB();
    //}

    //public class AntiCorruptionLayer : IAntiCorruptionLayer
    //{
    //    private ModuleB moduleB;

    //    public AntiCorruptionLayer()
    //    {
    //        // Initialize the actual ModuleB instance
    //        this.moduleB = new ModuleB();
    //    }

    //    public string GetDataFromModuleB()
    //    {
    //        // The ACL translates the data or method calls from ModuleA to ModuleB
    //        string dataFromModuleB = moduleB.GetData();
    //        // Perform any necessary translation or mapping here

    //        return dataFromModuleB;
    //    }

    //}
    //#endregion


    #region BillingExample

    // Define the interface for the BillingModule
    public interface IBillingService
    {
        void ProcessInvoice(InvoiceData invoice);
    }
    public class BillingModule : IBillingService
    {
        public void ProcessInvoice(InvoiceData invoice)
        {
            Console.WriteLine($"ProcessInvoice: {invoice.InvoiceNumber} {invoice.Amount}");
        }
    }
    // Define the interface for the AccountingModule
    public interface IAccountingService
    {
        void RecordTransaction(TransactionData transaction);
    }

    public class AccountingModule : IAccountingService
    {
        public void RecordTransaction(TransactionData transaction)
        {
            Console.WriteLine($"RecordTransaction: {transaction.TransactionId} - {transaction.TransactionAmount}");
        }
    }
    // Data structures for the BillingModule
    public class InvoiceData
    {
        public int InvoiceNumber { get; set; }
        public decimal Amount { get; set; }
        // Additional billing-related properties
    }

    // Data structures for the AccountingModule
    public class TransactionData
    {
        public int TransactionId { get; set; }
        public decimal TransactionAmount { get; set; }
        // Additional accounting-related properties
    }

    // Anti-Corruption Layer
    public class AntiCorruptionLayer
    {
        private readonly IAccountingService accountingService;

        public AntiCorruptionLayer(IAccountingService accountingService)
        {
            this.accountingService = accountingService;
        }

        public void ProcessInvoice(InvoiceData invoice)
        {
            // Translate InvoiceData to TransactionData
            TransactionData transaction = TranslateInvoiceToTransaction(invoice);

            // Call the AccountingModule to record the transaction
            accountingService.RecordTransaction(transaction);
        }

        private TransactionData TranslateInvoiceToTransaction(InvoiceData invoice)
        {
            TransactionData transaction = new TransactionData
            {
                TransactionId = invoice.InvoiceNumber,
                TransactionAmount = invoice.Amount,
                // Perform additional data translation if needed
            };
            return transaction;
        }


    }
    #endregion

}