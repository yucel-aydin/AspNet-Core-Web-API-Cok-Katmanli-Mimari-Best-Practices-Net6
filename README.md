

# Best Practise

## Http Method Tipi Seçimi
En yaygın kullanılan metotlar.
 1. GET: Data okuma için kullandığımız tiptir
 2. POST: Yeni bir nesne eklediğimizde kullandığımız tiptir.
 3. PUT: Bir nesne üzerinde güncelleme yapılaksa kullnadığımız tiptir.
 4. DELETE: Bir nesne silmek için kullandığımız tiptir.
 ## Doğru API / Endpoint Yapısı

<table>
<thead>
<tr>
<th>Method</th>
<th>Doğru</th>
<th>Yanlış</th>
</tr>
</thead>
<tbdoy>
<tr>
<td>GET</td>
<td>myapi.com/api/products</td>
<td>myapi.com/api/getproducts</td>
</tr>
<tr>
<td>GET</td>
<td>myapi.com/api/products/10</td>
<td>myapi.com/api/getproductnyid/10</td>
</tr>
<tr>
<td>POST</td>
<td>myapi.com/api/products</td>
<td>myapi.com/api/saveproduct</td>
</tr>
<tr>
<td>PUT</td>
<td>myapi.com/api/products</td>
<td>myapi.com/api/updateproduct</td>
</tr>
<tr>
<td>DELETE</td>
<td>myapi.com/api/products</td>
<td>myapi.com/api/deleteproduct</td>
</tr>
</tbdoy>
</table>
 
## Doğru Http Cevap Durum Kodları

 - **100 - Information Responses :** 100 ile başlıyan cevaplar bilgilendirici cevaplardır.
 - **200 - Successful Responses:** 200 ile başlıyan kodlar yapılan isteğin başarılı olduğunu gösterir.
 - **300 - Redirect Messages:** 300 ile başlıyanlar yönlendirme mesajlarını gösterir.
 - **400 - Client Error Responses:** Client isteği hatalıysa 400 lü kodlar kullanılır.
 - **500 - Server Error Responses:** Sunucumuzda bir hata oluştuğunda kullanılan hata kodlarıdır.
 
**En Çok Kullanılan Durum Kodları:** 
 - **200 - Ok:** Başarılı istek karşılığında geri dönülecek durum kodudur. Get metodu karşılığı dönülebilir. Get metotlarında kullanılabilir.
 - **201 - Created:** Yeni bir nesne eklendiğinde geri dönen durum kodudur. Post metotlarında kullanılabilir.
 - **204 - NoContent**: Eğer güncelleme veya silme işlemi varsa, yani PUT ve Delete metotlarının sonucunu dönmek için kullanılır.
 - **400 - BadRequest:** Client tarafında hatalı bir istek yaptığında dönülecek durum kodudur. Örneğin gönderilen data format hatası karşılığında dönülecek durum kodudur. Response da da hata ile ilgili açıklamayı dönebiliriz.
 - **401 - Unauthorized :** Client tarafında süresi bitmiş token gönderilmişse yada gönderilmemişse gibi durumlarda geri dönülen durum kodudur.
 - **403 - Forbid:** Client tarafından gönderilen tokenın ilgili işleme yetkisi yoktsa geri dönülen durum kodudur.
 - **500 - InternalServerError:** Suncuu tarafımızda oluşan hatalar için örneğin database bağlanma sorunu vs. gibi hatalarda geri dönülecek durum kodudur.
## Doğru Endpoint URL Yapısı
 - Get : https://myapi.com/categories/2/products -- Doğru 
 - Get : https://myapi.com/categories/2/products/5 --Hatalı parent-child ilişkilerinde bu şekilde yapılır olmamalıdır.
 - Get: https://myapi.com/products/5 -- Doğru
## Request İçerisinde Aynı Property'i Almaktan Kaçın
GET:    https://myapi.com/api/products/10
```csharp
	    [HttpPut("{id}")]
        Public IActionResult Update(Product product, int id)
        {
    	    // Güncelleme İşlemleri
    	}
```
Buradaki gibi hem url den Id alıp, hemde Product nesnesinin içinde Id alınıyor. Bir parametreyi iki kere almış olunuyor. Bunun yerine alttaki yöntemle yazılabilinir.

GET: https://myapi.com/api/products
```csharp
 [HttpPut]
 Public IActionResult Update(Product product)
 {
	    //Güncelleme İşlemleri
 }
```

## Asp.Net Core Uygulamasının startup.cs Dosyasını Mümkün Olduğunca Sade Bırak

    public void ConfigureServices(IServiceCollection services)
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
Startup.cs dosyamızdaki en önemli 2 methot olan ConfigureServices ve Configure mümkün olduğunca sade tutmak gerekir. Middleware ve servicelerimizi birer extension metot içerisine taşıyıp sadece metodu ilgili yerde çağırarak daha sade bir startup.cs dosyası yapılandırmış olur.
## Uygulamamızı mümkün olduğunca küçük parçalara böl
N Katmanlı mimari ile projeyi inşa etmek proje bazında parçalara bölmemizi sağlar. Yapacağımız uygulama küçük bir pojeyse ilerde büyüme olmayacaksa katmanlı mimari kullanmaya gerek yoktur.
Örneğin ;
**MyApp.Web**       	=> Web Uygulaması
**MyApp.API** 			=> API uygulaması
**MyApp.Core** 			=> ClassLibrary (Temel nesnelerimiz tutarız. )
**MyApp.Data** 			=> ClassLibrary (Entity Framework işlmelerini içerir)
**MyApp.Service** 		=> ClassLibrary (Data katmanından gelen verileri api a ulaştırmak için)
**MyApp. Logging** 	=> ClassLibrary (Loglama ile ilgili işlemleri gerçekleştirdiğimiz katman)

## Controller sınıflarınız mümkün oldukça temiz tutun. Business kodu bulundurmayın.
Controllerda yardımcı metotlar olmamalı. Sadece Action metotlar olmalı . 
```csharp
		[Route("api/[controller]")]
        [ApiController]
        public class ProductsController : ControllerBase
        {
            private readonly IProductService _productService;
            private readonly IMapper _mapper;
            public ProductsController(IProductService productService, IMapper mapper)
            {
                _productService = productService;
                _mapper = mapper;
            }

            [HttpGet]
            public async Task<IActionResult> GetAllProduct()
            {
                var products = await _productService.GetAll();
                return Ok(_mapper.Map<IEnumerable<ProductDto>>(products))
            }

            [ValidationFilter]
            [ServiceFilter(typeof(NotFoundFilter))]
            [HttpGet("{id}")]
            public async Task<IActionResult> GetProductById(int id)
            {
                var product = await _productService.GetById(id);
                return Ok(_mapper.Map<ProductDto>(product));
            }
        }
```

## Action methodlarımızı mümkün oldukça temiz tutun. Business kodu bulundurmayın.
```csharp
[HttpGet]
public async Task<IActionResult> GetAllProduct()
{
	var products = await _productService.GetAll();
	return Ok(_mapper.Map<IEnumerable<ProductDto>>(products))
}

```
## Hataları global olarak ele alın. Action methodlar içerisinde try-catch blokları kullanmayın.

## Tekrar eden kodlardan kurtulmak için filter kullan.


```csharp
[ServiceFilter(typeof(NotFoundFilter))]
[HttpGet("{id}")]
public async Task<IActionResult> GetProductById(int id)
{
	///İşlemler
}

[ValidationFilter]
[HttpPost]
public async Task<IActionResult> Create(ProductDto product)
{
	//İşlemler
}

[ValidationFilter]
[HttpPost]
public async Task<IActionResult> Update(ProductDto product)
{
	//İşlemler
}
`````


    [ServiceFilter(typeof(NotFoundFilter))]
   
NotFoundFilter ile id üzerinde veritabanında bu id li product varmı kontrolünü sağlar.

     [ValidationFilter]
ValidationFilter ile  clienttan gelen ProductDto modelinde hatalı veri varsa bunun kontrolünü yapmak için.

## Action methodlardan direk olarak model sınıflarınızı dönmeyin.

İlgili modellerin DTO(Data Transfer Object) sınıflarını dönün. Yani direk modeli dönmek yerine sadece clientın ihtiyacına özel bir dto dönmeliyiz.
**Modelimiz:**
```csharp
Public class Product {
	public int Id {get; set;}
	public string name {get; set;}
	public decimal Price {get; set;}
	public int Stock {get; set;}
	public int CategoryId {get; set;}
	public bool IsDeleted {get; set;}
	public string InnerBarkod {get; set;}
}
````
**Clienta Dönülecek DTO :**
```csharp
Public class ProductDto {
	public int Id {get; set;}
	public string name {get; set;}
	public decimal Price {get; set;}
	public int Stock {get; set;}
	public int CategoryId {get; set;}	
}
````

## N-Layer Proje Yapısı Örnek

 1. **Core Katmanı :** Model(Entity), DTOs, Repository Interfaces, Service Interfaces, UnitOfWork Interfaces
 2. **Repository Katmanı:**  Migrations, Seeds, Repository Impl, UnitOfWork Impl ⇒ Reference Core Layer
 3. **Service Katmanı:**  Mapping, Service Impl, Validations, Exceptions ⇒ Reference Repository Layer
 5. **API :**  Reference Service Layer
 6. **WEB :** Reference Service Layer Or Use API

_Separation of concern_ (SoC) (İlgili kod ilgili yerde bulunacak) prensibine uygun N-Layer proje yapısı.