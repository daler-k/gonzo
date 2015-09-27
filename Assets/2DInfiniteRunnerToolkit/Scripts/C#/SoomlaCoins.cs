using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Soomla.Store.Coins {
	
	/// <summary>
	/// This class defines our game's economy, which includes virtual goods, virtual currencies
	/// and currency packs, virtual categories
	/// </summary>
	public class SoomlaCoins : IStoreAssets{
		
		
		/// <summary>
		/// see parent.
		/// </summary>
		public int GetVersion() {
			return 0;
		}
		
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualCurrency[] GetCurrencies() {
			return new VirtualCurrency[]{COINS_CURRENCY};
		}
		
		public static VirtualCurrency COINS_CURRENCY = new VirtualCurrency(
			"Coins",										// name
			"",												// description
			COINS_CURRENCY_ITEM_ID							// item id
			);
		
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualGood[] GetGoods() {
			return new VirtualGood[] {};
		}
		
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualCurrencyPack[] GetCurrencyPacks() {
			return new VirtualCurrencyPack[] {THOUSANDCOINS_PACK};
		}
		
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualCategory[] GetCategories() {
			return new VirtualCategory[]{};
		}
		
		/** Static Final Members **/
		
		
		public const string THOUSANDCOINS_PACK_PRODUCT_ID = "1000_coins_pack_gonzo";
		public const string COINS_CURRENCY_ITEM_ID      = "currency_coins";
		
		
		/** Virtual Currencies **/
		
		
		public static VirtualCurrencyPack THOUSANDCOINS_PACK = new VirtualCurrencyPack(
			"1000 Coins",                                 // name
			"Test item unavailable",                 		// description
			"1000_coins",                                 // item id
			3000,                                           // number of currencies in the pack
			COINS_CURRENCY_ITEM_ID,                        // the currency associated with this pack
			new PurchaseWithMarket(THOUSANDCOINS_PACK_PRODUCT_ID, 59)
			);
		
		
	}
	
	
	
}