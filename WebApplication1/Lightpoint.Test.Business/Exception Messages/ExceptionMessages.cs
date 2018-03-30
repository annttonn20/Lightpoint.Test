using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Lightpoint.Test.Business.Exception_Messages
{
    public static class ExceptionMessages
    {
        public static string CannotAddStore()
        {
            switch (CultureInfo.CurrentCulture.Name)
            {
                case "ru-RU":
                    {
                        return "Не возможно обновить базу данных. Такой магазин уже существует ";
                    }
                case "en-GB":
                    {
                        return "It is not possible to update the database. This store already exists";
                    }
                default:
                    break;
            }
            return null;
        }

        public static string CannotAddProduct()
        {
            switch (CultureInfo.CurrentCulture.Name)
            {
                case "ru-RU":
                    {
                        return "Не возможно обновить базу данных. Такой продукт уже существует ";
                    }
                case "en-GB":
                    {
                        return "It is not possible to update the database. This product already exists";
                    }
                default:
                    break;
            }
            return null;
        }



        public static string CannotAddProductToStore()
        {
            switch (CultureInfo.CurrentCulture.Name)
            {
                case "ru-RU":
                    {
                        return "Не возможно обновить базу данных. Такой продукт уже существует у магазина ";
                    }
                case "en-GB":
                    {
                        return "It is not possible to update the database. This product already exists in the store";
                    }
                default:
                    break;
            }
            return null;
        }

        public static string CannotAddProductToStorePRODUCT()
        {
            switch (CultureInfo.CurrentCulture.Name)
            {
                case "ru-RU":
                    {
                        return "Не удается добавить продукт магазину. Такого продукта не существует";
                    }
                case "en-GB":
                    {
                        return "Can not add product to the store. This product does not exist";
                    }
                default:
                    break;
            }
            return null;
        }

        public static string CannotRemoveProductFromStorePRODUCT()
        {
            switch (CultureInfo.CurrentCulture.Name)
            {
                case "ru-RU":
                    {
                        return "Не удается удалить продукт из магазину. Такого продукта не существует";
                    }
                case "en-GB":
                    {
                        return "Can not remove product from the store. This product does not exist";
                    }
                default:
                    break;
            }
            return null;
        }

        public static string CannotAddProductToStoreSTORE()
        {
            switch (CultureInfo.CurrentCulture.Name)
            {
                case "ru-RU":
                    {
                        return "Не удается добавить продукт магазину. Такого магазина не существует";
                    }
                case "en-GB":
                    {
                        return "Can not add product to the store. This store does not exist";
                    }
                default:
                    break;
            }
            return null;
        }

        public static string CannotRemoveProductFromStoreSTORE()
        {
            switch (CultureInfo.CurrentCulture.Name)
            {
                case "ru-RU":
                    {
                        return "Не удается убрать продукт из магазину. Такого магазина не существует";
                    }
                case "en-GB":
                    {
                        return "Can not remove product from the store. This store does not exist";
                    }
                default:
                    break;
            }
            return null;
        }
    }
}
