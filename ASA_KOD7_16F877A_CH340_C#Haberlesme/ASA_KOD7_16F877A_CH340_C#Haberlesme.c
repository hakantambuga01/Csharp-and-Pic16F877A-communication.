#include <16F877A.h>
#device ADC=10
#fuses XT,NOWDT,NOPROTECT,NOBROWNOUT,NOLVP,NOPUT,NOWRT,NODEBUG,NOCPD

#use delay(crystal=4000000)
#use rs232(baud=9600,xmit=PIN_C6,rcv=PIN_C7,bits=8,parity=N, stop=1)

char gelenVeri;
int16 k;
int sag,sol;
int stepMotor_hizi=6;
int i,m;
const int yarim_adim[]={0x01,0x03,0x02,0x06,0x04,0x0C,0x08,0x09}; // Step motor yar�m ad�m d�n�� ad�mlar�
const int ters_yarim_adim[]={0x09,0x08,0x0C,0x04,0x06,0x02,0x03,0x01};

int stepSag=0,stepSol=0;
#int_rda
void serihaberlesme_kesmesi()
{
   disable_interrupts(int_rda);
   gelenVeri = getchar();
   ///////////////////////////////////////////////////////
   ///////////////////////////////////////////////////////
   ///////////////////////////////////////////////////////
   ///////////////////////////////////////////////////////
   ///////////////////////////////////////////////////////
   ///////////////////////////////////////////////////////
   ///////////////////////////////////////////////////////
   ///////////////////////////////////////////////////////
   ///////////////////////////////////////////////////////
   //////////////////DC MOTOR KONTROL�////////////////////
   if(gelenVeri == 'D') //sag d�n
      { 
         k=0;
         sag=1;
         sol=0;
         output_low(pin_c0);
         set_pwm2_duty(k);
         delay_ms(100);
      }
    if(gelenVeri == 'A')//sola d�n
      {
         k=250;
         sol=1;
         sag=0;
         output_high(pin_c0);
         set_pwm2_duty(k);
         delay_ms(100);
      }
      if(sag==1)
      {
      if(gelenVeri == 'W')// pwm artt�r
      { 
         k=k+10;             
         if(k>=240)k=240;
         set_pwm2_duty(k);
         delay_ms(100);
      }
      if(gelenVeri == 'S')//pwm azalt
      { 
         k=k-10;   
         if(k<=10)k=10;
         set_pwm2_duty(k);      
         delay_ms(100);
      }
      }
      if(sol==1)
      {
      if(gelenVeri == 'S')// pwm azalt
      { 
         k=k+10;             
         if(k>=240)k=240;
         set_pwm2_duty(k);
         delay_ms(100);
      }
      if(gelenVeri == 'W')//pwm artt�r
      { 
         k=k-10;   
         if(k<=10)k=10;
         set_pwm2_duty(k);      
         delay_ms(100);
      }
      }
      if(gelenVeri == 'Z')//motorlar� durdur
      { 
         k=0;         
         output_low(pin_c0);
         set_pwm2_duty(k);
         sag=0;
         sol=0;
         delay_ms(100);
      }
   ///////////////////////////////////////////////////////
   ///////////////////////////////////////////////////////
   ///////////////////////////////////////////////////////
   ///////////////////////////////////////////////////////
   ///////////////////////////////////////////////////////
   ///////////////////////////////////////////////////////
   //////////////////STEP MOTOR KONTROL�//////////////////
   if(gelenVeri == 'H') 
   {output_b(0x00);  delay_ms(300); stepSag=1; stepSol=0; output_b(0x00); }//sag d�n
   
   if(gelenVeri == 'F')
   {output_b(0x00);  delay_ms(300); stepSol=1; stepSag=0; output_b(0x00); }//sol d�n
   
   if(gelenVeri == 'T')
   {stepMotor_hizi=stepMotor_hizi-1; if(stepMotor_hizi<=1)stepMotor_hizi=1; delay_ms(10); }//h�zlan
   
   if(gelenVeri == 'G')
   {stepMotor_hizi=stepMotor_hizi+1; if(stepMotor_hizi>=5)stepMotor_hizi=5; delay_ms(10); }//yava�la
   
      if(gelenVeri == 'X')  // Motor dur
   {
      stepSag=0;
      stepSol=0;
      
     delay_ms(200);
   }
   


   
      
}



   ///////////////////////////////////////////////////////
   ///////////////////////////////////////////////////////
   ///////////////////////////////////////////////////////
   ///////////////////////////////////////////////////////
   ///////////////////////////////////////////////////////
   ///////////////////////////////////////////////////////
   ///////////////////////////////////////////////////////
   ///////////////////////////////////////////////////////
void main()
{
   setup_psp(PSP_DISABLED);        // PSP birimi devre d���
   setup_spi(SPI_SS_DISABLED);     // SPI birimi devre d���
   setup_timer_1(T1_DISABLED);     // T1 zamanlay�c�s� devre d���
   Setup_timer_2(T2_DIV_BY_16,253,1); ; // T2 zamanlay�c�s� devre d���
   setup_adc_ports(NO_ANALOGS);    // ANALOG giri� yok
   setup_adc(ADC_OFF);             // ADC birimi devre d���
   setup_CCP1(CCP_OFF);            // CCP1 birimi devre d���
   setup_CCP2(CCP_PWM); 
   
//!   output_b(0x00);  
   output_low(pin_c0);
   set_pwm2_duty(0);
   delay_ms(100);
   
   enable_interrupts(GLOBAL);
   while(TRUE)
   {
      enable_interrupts(int_rda);
      
      
      
    if(stepSag == 1)
   {
      i=0;
      for(i=0; i<8; i++)
        {
         output_b(yarim_adim[i]);  // Step motor yar�m ad�m ileri
         delay_ms(stepMotor_hizi);
         enable_interrupts(int_rda);
      
        }
   }

   
   if(stepSol == 1)
   {
      m=0;
      for(m=0; m<8; m++)
        {
         output_b(ters_yarim_adim[m]);  // Step motor yar�m ad�m ileri
         delay_ms(stepMotor_hizi);
         enable_interrupts(int_rda);
      
        }
   
   }

   }}


