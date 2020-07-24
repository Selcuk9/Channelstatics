﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Source.Logic
{
    /// <summary>
    /// Отвечаает за непосредственный сбор данных
    /// </summary>
    public class StatisticsProcessor
    {
        /// <summary>
        /// Запускаем ежечасно, желательно в отдельном потоке, поставить приоритет потока на высокий, а потом выполнять параллельно.
        /// </summary>
        public void Process()
        {
            //1) Взять из базы список всех каналов.
            //2) Добавить в базу кол-во подписчиков канала на текущий час.
            //3) Запустить цикл по каналам и на каждой итерации сделать следующее
              //3.1) Взять все посты у канала, через запрос.
              //3.2) По каждому посту записать в базу кол-во просмотров на текущий момент.
              //3.3) Сделать запись в  базу по количеству просмотров поста.
        }

    }
}
