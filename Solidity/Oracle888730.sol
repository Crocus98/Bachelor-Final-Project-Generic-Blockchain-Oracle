pragma solidity ^0.7.3;
//SPDX-License-Identifier: MIT

//pragma experimental ABIEncoderV2;


import "./Client888730.sol";


//Matteo Savino
contract Oracle888730 {
    address payable public oracleOwner;
    string public OracleName = "GenericOracle";

    mapping (address => bool) private oracleSecondaryAddresses;

    constructor() {
        oracleOwner = msg.sender;
    }

    function AddOracleSecondaryAccount(address _newOracleSecondaryAccount) public onlyOracleOwner {
        oracleSecondaryAddresses[_newOracleSecondaryAccount] = true;
    }

    event GenericPaymentEvent(
        address _sender,
        uint _value
    );

    event ServicePaymentEvent(
        address _sender,
        uint _value,
        string _serviceType
    );

    event RequestEvent(
         address _sender,
         string _requestService,
         uint _requestServiceType
    );

    event SubscribeEvent(
        address _sender,
        string _subscribeService,
        uint _subscribeServiceType
    );

    modifier onlyOracleOwner() {
        require ( msg.sender == oracleOwner || oracleSecondaryAddresses[msg.sender] , "You are not the owner of the oracle contract" );
        _;
    }

    //receive function che viene chiamata solo se si inviano ether senza alcun dato
    receive() external payable {
        //I pagamenti ricevuti saranno tenuti come donazione
        oracleOwner.transfer(msg.value);
        emit GenericPaymentEvent(msg.sender, msg.value);
        //Queste f hanno limite di 2300 gas limit non si può ne trasferire ne scrivere su chain
        //oracleOwner.transfer(msg.value);
    }

    //Fallback viene chiamata solo se non esiste nessun'altra funzione payable (ultima spiaggia)
    fallback() external payable {
        //I pagamenti ricevuti saranno tenuti come donazione
        oracleOwner.transfer(msg.value);
        emit GenericPaymentEvent(msg.sender, msg.value);
        //Queste f hanno limite di 2300 gas limit non si può ne trasferire ne scrivere su chain
    }

    function PayService(string calldata _service) external payable {
        //Attenzione se utilizzo .call invece di .transfer per evitare falle di sicurezza
        //https://ethereum.stackexchange.com/questions/68861/why-cant-we-use-transfer-to-get-a-fallback-from-a-contract
        emit ServicePaymentEvent (msg.sender, msg.value, _service);
        oracleOwner.transfer(msg.value);
    }

    //Push based inbound oracle request
    function GetSubscribeRequest(string calldata _service, uint _serviceType) external {
        emit SubscribeEvent(msg.sender, _service, _serviceType);
    }

    //Pull based inbound oracle request
    function GetRequest(string calldata _service, uint _serviceType) external {
        emit RequestEvent(msg.sender, _service, _serviceType);
    }
    
    //Response
    function SendResponse(address _clientAddress,string memory _service, uint  _serviceType, string memory _value) public onlyOracleOwner {
        //_value ovvero il valore della risposta potrebbe essere un JSON e contenere più informazioni raggruppate
        Client888730 client = Client888730(address(_clientAddress));
        client.GetResponse(_service, _serviceType, _value);
    }

    //Public: functions are part of the contract interface and can be either called internally or via messages. For public state variables, an automatic getter function (see below) is generated.
    //Internal: functions and state variables can only be accessed internally (i.e. from within the current contract or contracts deriving from it), without using this
    //Private: Private functions and state variables are only visible for the contract they are defined in and not in derived contracts.
    //External: they can be called from other contracts and via transactions. An external function f cannot be called internally (i.e. f() does not work, but this.f() works).
}