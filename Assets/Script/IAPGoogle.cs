using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using QuantumTek.EncryptedSave;
using UnityEngine.UI;



// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
public class IAPGoogle : MonoBehaviour, IStoreListener
{

    public GooglePlayScript gps;

    public Mainmenu main_Menu;

    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    private float money;

    public GameObject premiumShop;
    public Text[] buttonTextPremiumShop;
    public Text[] buttonTextNotEnoughMoney;

    // Product identifiers for all products capable of being purchased: 
    // "convenience" general identifiers for use with Purchasing, and their store-specific identifier 
    // counterparts for use with and outside of Unity Purchasing. Define store-specific identifiers 
    // also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

    // General product identifiers for the consumable, non-consumable, and subscription products.
    // Use these handles in the code to reference which product to purchase. Also use these values 
    // when defining the Product Identifiers on the store. Except, for illustration purposes, the 
    // kProductIDSubscription - it has custom Apple and Google identifiers. We declare their store-
    // specific mapping to Unity Purchasing's AddProduct, below.

    public static string PRODUCT_1000_GOLD = "1000gold";
    public static string PRODUCT_5000_GOLD = "5000gold";
    public static string PRODUCT_10000_GOLD = "10000gold";
    public static string PRODUCT_50000_GOLD = "50000gold";
    public static string PRODUCT_100000_GOLD = "100000";
    public static string PRODUCT_500000_GOLD = "500000gold";
    public static string PRODUCT_1000000_GOLD = "1000000gold";


    // Google Play Store-specific product identifier subscription product.
   // private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";

    void Start()
    {

        main_Menu = GameObject.Find("Canvas").GetComponent<Mainmenu>();

        money = ES_Save.Load<float>("money");

        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
    }


    public void RefreshMoneyLocalized()
    {
        
            buttonTextPremiumShop[0].text = "Buy: " + m_StoreController.products.WithID(PRODUCT_1000_GOLD).metadata.localizedPriceString;
            buttonTextPremiumShop[1].text = "Buy: " + m_StoreController.products.WithID(PRODUCT_5000_GOLD).metadata.localizedPriceString;
            buttonTextPremiumShop[2].text = "Buy: " + m_StoreController.products.WithID(PRODUCT_10000_GOLD).metadata.localizedPriceString;
            buttonTextPremiumShop[3].text = "Buy: " + m_StoreController.products.WithID(PRODUCT_50000_GOLD).metadata.localizedPriceString;
            buttonTextPremiumShop[4].text = "Buy: " + m_StoreController.products.WithID(PRODUCT_100000_GOLD).metadata.localizedPriceString;
            buttonTextPremiumShop[5].text = "Buy: " + m_StoreController.products.WithID(PRODUCT_500000_GOLD).metadata.localizedPriceString;
            buttonTextPremiumShop[6].text = "Buy: " + m_StoreController.products.WithID(PRODUCT_1000000_GOLD).metadata.localizedPriceString;
        
    }
    public void RefreshMoneyNEMLocalized()
    {

        buttonTextNotEnoughMoney[0].text = "Buy: " + m_StoreController.products.WithID(PRODUCT_1000_GOLD).metadata.localizedPriceString;
        buttonTextNotEnoughMoney[1].text = "Buy: " + m_StoreController.products.WithID(PRODUCT_5000_GOLD).metadata.localizedPriceString;
        buttonTextNotEnoughMoney[2].text = "Buy: " + m_StoreController.products.WithID(PRODUCT_10000_GOLD).metadata.localizedPriceString;
        buttonTextNotEnoughMoney[3].text = "Buy: " + m_StoreController.products.WithID(PRODUCT_50000_GOLD).metadata.localizedPriceString;
        buttonTextNotEnoughMoney[4].text = "Buy: " + m_StoreController.products.WithID(PRODUCT_100000_GOLD).metadata.localizedPriceString;
        buttonTextNotEnoughMoney[5].text = "Buy: " + m_StoreController.products.WithID(PRODUCT_500000_GOLD).metadata.localizedPriceString;
        buttonTextNotEnoughMoney[6].text = "Buy: " + m_StoreController.products.WithID(PRODUCT_1000000_GOLD).metadata.localizedPriceString;

    }

    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add a product to sell / restore by way of its identifier, associating the general identifier
        // with its store-specific identifiers.
        builder.AddProduct(PRODUCT_1000_GOLD, ProductType.Consumable);
        builder.AddProduct(PRODUCT_5000_GOLD, ProductType.Consumable);
        builder.AddProduct(PRODUCT_10000_GOLD, ProductType.Consumable);
        builder.AddProduct(PRODUCT_50000_GOLD, ProductType.Consumable);
        builder.AddProduct(PRODUCT_100000_GOLD, ProductType.Consumable);
        builder.AddProduct(PRODUCT_500000_GOLD, ProductType.Consumable);
        builder.AddProduct(PRODUCT_1000000_GOLD, ProductType.Consumable);


        // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
        // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    public void Buy1000gold()
    {
        BuyProductID(PRODUCT_1000_GOLD);


    }
    public void Buy5000gold()
    {
        BuyProductID(PRODUCT_5000_GOLD);

    }
    public void Buy10000gold()
    {
        BuyProductID(PRODUCT_10000_GOLD);

    }
    public void Buy50000gold()
    {
        BuyProductID(PRODUCT_50000_GOLD);

    }
    public void Buy100000gold()
    {
        BuyProductID(PRODUCT_100000_GOLD);

    }
    public void Buy500000gold()
    {
        BuyProductID(PRODUCT_500000_GOLD);

    }
    public void Buy1000000gold()
    {
        BuyProductID(PRODUCT_1000000_GOLD);

    }


    //public void BuyNonConsumable()
    //{
    //    // Buy the non-consumable product using its general identifier. Expect a response either 
    //    // through ProcessPurchase or OnPurchaseFailed asynchronously.
    //    BuyProductID(kProductIDNonConsumable);
    //}


    //public void BuySubscription()
    //{
    //    // Buy the subscription product using its the general identifier. Expect a response either 
    //    // through ProcessPurchase or OnPurchaseFailed asynchronously.
    //    // Notice how we use the general product identifier in spite of this ID being mapped to
    //    // custom store-specific identifiers above.
    //    BuyProductID(kProductIDSubscription);
    //}


    void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }


    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }



    private void GoogleCloudeSave()
    {
        if (gps == null)
        {
            gps = GameObject.Find("GooglePlayServices").GetComponent<GooglePlayScript>();
            gps.SaveToCloud();
        }
        else
        {
            gps.SaveToCloud();
        }
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // A consumable product has been purchased by this user.
        if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_1000_GOLD, StringComparison.Ordinal))
        {
            money = ES_Save.Load<float>("money");
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            money += 1000;
            ES_Save.Save(money, "money");
            main_Menu.RoundMoney();
            GoogleCloudeSave();


        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_5000_GOLD, StringComparison.Ordinal))
        {
            money = ES_Save.Load<float>("money");
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            money += 10000;
            ES_Save.Save(money, "money");
            main_Menu.RoundMoney();
            GoogleCloudeSave();

        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_10000_GOLD, StringComparison.Ordinal))
        {
            money = ES_Save.Load<float>("money");
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            money += 15000;
            ES_Save.Save(money, "money");
            main_Menu.RoundMoney();
            GoogleCloudeSave();

        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_50000_GOLD, StringComparison.Ordinal))
        {
            money = ES_Save.Load<float>("money");
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            money += 50000;
            ES_Save.Save(money, "money");
            main_Menu.RoundMoney();
            GoogleCloudeSave();

        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_100000_GOLD, StringComparison.Ordinal))
        {
            money = ES_Save.Load<float>("money");
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            money += 100000;
            ES_Save.Save(money, "money");
            main_Menu.RoundMoney();
            GoogleCloudeSave();

        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_500000_GOLD, StringComparison.Ordinal))
        {
            money = ES_Save.Load<float>("money");
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            money += 500000;
            ES_Save.Save(money, "money");
            main_Menu.RoundMoney();
            GoogleCloudeSave();

        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_1000000_GOLD, StringComparison.Ordinal))
        {
            money = ES_Save.Load<float>("money");
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            money += 1000000;
            ES_Save.Save(money, "money");
            main_Menu.RoundMoney();
            GoogleCloudeSave();

        }
        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }

        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}
