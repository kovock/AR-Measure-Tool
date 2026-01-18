*📏Measuring AR \- Aplicativo de AR para Medição de Distância

Measuring AR é um aplicativo para o sistema Android que realiza medições de distância em tempo real por meio de Realidade Aumentada. A aplicação foi desenvolvida utilizando Unity 6, AR Foundation e ARCore.

**Visão Geral**  
O objetivo deste projeto é desenvolver uma aplicação para o sistema Android baseada em Realidade Aumentada, capaz de realizar medições de distância em tempo real em ambientes reais. A proposta do trabalho é aplicar, de forma prática, os conceitos estudados na disciplina de Computação Gráfica, incluindo rastreamento espacial, detecção de superfícies e interação entre o usuário e o ambiente.  
**Funcionalidades do aplicativo:**

* Detecção de superfícies planas no ambiente real por meio da câmera do dispositivo.  
* Posicionamento interativo de pontos de medição utilizando toques na tela.  
* Cálculo automático da distância entre os pontos selecionados.  
* Exibição da medida de distância em tempo real na interface do aplicativo.  
* Possibilidade de realizar múltiplas medições de forma consecutiva.  
* Interação intuitiva entre o usuário e o ambiente em Realidade Aumentada.

**Tecnologias Utilizadas**  
**Engine Unity 6**: responsável pela criação do ambiente gráfico e pela integração dos elementos de Realidade Aumentada

**AR Foundation**: utilizado como camada de abstração para funcionalidades de AR

**ARCore**: empregado para rastreamento de movimento, detecção de superfícies e compreensão do ambiente real em dispositivos Android

**Linguagem de Programação C\#**: utilizada para implementação da lógica do aplicativo; e o sistema operacional Android, plataforma alvo da aplicação.

**Guia de Instalação**

**Opção A: Compilar o Código**

1. Instalar o Unity Hub no computador, pode fazer o download através do link: [https://unity.com/download](https://unity.com/download)  
2. No Unity Hub, instalar a versão Unity 6.0 (6000.0.65f1) utilizada no projeto, garantindo que os módulos de build para Android estejam selecionados (Android Build Support, SDK, NDK e OpenJDK).   
3. Clonar ou baixar o repositório do projeto Measuring AR para a máquina local  
4. Abrir o Unity Hub e adicionar a pasta do projeto “MedidorAR”  
5. Abra o projeto e aguarde carregar.  
6. Com o projeto aberto faça o passo a passo a seguir:  
   1. Para habilitar a cena,Vá em “Assets” clique em “Scenes” e de um clique duplo em “CenaMedicaoAr” o 

   2. Para executar a aplicação vamos acessar as configurações de Build Profiles e selecionar a plataforma Android. Clique em File depois em Build Profiles.  
   3. Clique em “Android” e depois clique em “Switch Platform” para fazer a mudança.  
   4. Conecte um dispositivo Android ao computador via USB.  
   5. Ative o Modo Desenvolvedor e a Depuração USB no dispositivo Android.  
   6. Clique em Run Device e procure pelo seu dispositivo android, caso não encontre clique em Refresh.

7. Para executar a aplicação clique em “file” e depois em “Build and Run” e Salve o Apk em uma pasta da sua preferência.  
 


   

**Opção B: APK**

1. Faça o download do APK no seu dispositivo android  
2. Se o Android bloquear, seleciona Definições \> Permitir desta fonte.  
3. Clica em Instalar.  
4. Abra o aplicativo e dê as permissões para utilização da câmera.  
5. Teste o Aplicativo

**Como Usar o Aplicativo**

1. Abra o aplicativo.  
2. Conceda as permissões de acesso à câmera quando solicitado.  
3. Aponte a câmera do dispositivo para o ambiente ao redor até que o aplicativo detecte uma superfície plana.  
4. Toque na tela e arraste para posicionar o ponto de medição no espaço.  
5. A distância entre os pontos será calculada automaticamente e exibida na tela.  
6. Clique no botão com o ícone de lixeira para limpar as medidas.  
7. Caso deseje ocultar a malha da superfície, clique nos três pontos no canto superior direito e desative a opção “Visualize Surfaces”.
