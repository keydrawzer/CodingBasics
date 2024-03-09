interface BaseProductsService
{
    List<ProductModel>? GetAll();
    List<ProductModel>? GetProductByName(string name);

    List<ProductModel>? GetProductByCategoryType(string categoryType); 

}