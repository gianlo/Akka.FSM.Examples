import actors.TurnstileActor
import akka.actor.{ActorSystem, Props}
import messages._

/**
  * Created by gianlo.fagiolo on 10/06/2016.
  */
object program {
  def main(args: Array[String]) {
    val system = ActorSystem("TurnstileSimulator")
    val turnstileActor = system.actorOf(Props(classOf[TurnstileActor]), "Turnstile")
    turnstileActor ! PushBar
  }
}
