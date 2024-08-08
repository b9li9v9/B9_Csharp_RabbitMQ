```
流程
1.下载RabbitMQ 配路径
2.下载RabbitMQ浏览器 rabbitmq-plugins enable rabbitmq_management  
	http://localhost:15672  账号密码默认是：guest/guest  
	guest 用户是本地账号如果 不在同一台服务器上无法连接 需要新建一个账号并且给这个账号对应的权限
3.NuGet RabbitMQ.Client
4.写代码
```

```
大概用法

	RabbitMQ 就是数据的中转站
	数据的生产者，消费者，二者之间的桥梁
	概念 连接 通道 交换器 队列 绑定等简单来说就是 数据路由 的问题，还有怎么取 取多少 取后处理完要不要给答复之类的
	通道就像连接邮局的路，交换器像是一个大邮局(RabbitMQ)内部的不同分部，绑定队列就像监听分部
	队列 (Queue) 是 RabbitMQ 中用来存储消息的容器，消息会被发送到队列中，然后被消费者消费。
	通道 (Channel) 则是用来发送和接收消息的虚拟连接，一个连接可以创建多个通道。

	生产者推送的消息可以是一个消费者完成，
	也可以是一群消费者齐力消灭，就像码头上的工人卸货，
	领了任务就走，任务完成后回来交差，再继续领..，直至货船被取空  -> “Work Queues”（工作队列）这种方式没有使用到交换器
	
	生产者推送的消息也可以像是微信群发信息，所有的消费者都会收到这条数据 -> "fanout"

	direct好理解，绑定键(binding key) 与 路由键(routing key) 匹配就可以互通
	
	topic我觉得更像是direct的升级，可以通过*和#定义监听端的匹配规则

	direct, topic, headers， fanout.
	直接    主题    标头       扇出

	交换器类型	路由规则	                                                      优势	     劣势        
	Direct	    队列绑定到一个特定的路由键，消息只会被发送到绑定了该路由键的队列	      简单易用	 灵活度较低
	Fanout	    消息广播到所有绑定的队列	最简单，所有队列都收到消息	              效率较低，	 浪费资源
	Topic	    使用通配符匹配路由键，消息会被发送到所有匹配的队列	灵活度高	          实现复杂，	 需要理解通配符
	Headers	    根据消息的 Headers 属性进行路由	最灵活，可以匹配任意属性	          实现复杂，	 需要定义 Headers 属性
```


```
资源
阿里云教程 https://developer.aliyun.com/article/769883
博客园教程 https://www.cnblogs.com/xiongyang123/p/14272115.html
RabbitMQ官网 C# https://www.rabbitmq.com/tutorials/tutorial-one-dotnet

```
