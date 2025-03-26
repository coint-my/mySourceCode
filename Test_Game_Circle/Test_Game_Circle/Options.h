#define WIDTH 800
#define HEIGHT 600

//максимальное число примитивов и обьектов
#define MAX_OBJECT 1000
//максимум тригеров
#define MAX_TRIGGER 4
//количество блоков
#define COUNT_BLOOKS 300
//текущие обьекты
static int CURRENT_OBJECT = 0;
//текущие триггеры
static int CURRENT_TRIGGER = 0;
//текущие ключи
static int CURRENT_KEYS = 0;
//ширина и высота тайла
static float W_CELL = 10; 
static float H_CELL = 10;
//максимум ключей
#define MAX_KEYS 20