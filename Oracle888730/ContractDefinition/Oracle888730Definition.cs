using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace Oracle888730.Contracts.Oracle888730.ContractDefinition
{


    public partial class Oracle888730Deployment : Oracle888730DeploymentBase
    {
        public Oracle888730Deployment() : base(BYTECODE) { }
        public Oracle888730Deployment(string byteCode) : base(byteCode) { }
    }

    public class Oracle888730DeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60c0604052600d60808190526c47656e657269634f7261636c6560981b60a090815261002e9160019190610053565b5034801561003b57600080fd5b50600080546001600160a01b031916331790556100f4565b828054600181600116156101000203166002900490600052602060002090601f01602090048101928261008957600085556100cf565b82601f106100a257805160ff19168380011785556100cf565b828001600101855582156100cf579182015b828111156100cf5782518255916020019190600101906100b4565b506100db9291506100df565b5090565b5b808211156100db57600081556001016100e0565b6109a3806101036000396000f3fe6080604052600436106100745760003560e01c80636b28d8601161004e5780636b28d86014610257578063a578be16146102d2578063d1f429c414610303578063e756d18514610451576100f0565b80630fa4bf721461012a5780631475e99b146101b45780635f08ec40146101e9576100f0565b366100f057600080546040516001600160a01b03909116913480156108fc02929091818181858888f193505050501580156100b3573d6000803e3d6000fd5b506040805133815234602082015281517f890ae3b9a5e6da809bace04153fc525da24c96e933acfa00a0cc2dcc2f186005929181900390910190a1005b600080546040516001600160a01b03909116913480156108fc02929091818181858888f193505050501580156100b3573d6000803e3d6000fd5b34801561013657600080fd5b5061013f6104cc565b6040805160208082528351818301528351919283929083019185019080838360005b83811015610179578181015183820152602001610161565b50505050905090810190601f1680156101a65780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b3480156101c057600080fd5b506101e7600480360360208110156101d757600080fd5b50356001600160a01b0316610559565b005b6101e7600480360360208110156101ff57600080fd5b810190602081018135600160201b81111561021957600080fd5b82018360208201111561022b57600080fd5b803590602001918460018302840111600160201b8311171561024c57600080fd5b5090925090506105e0565b34801561026357600080fd5b506101e76004803603604081101561027a57600080fd5b810190602081018135600160201b81111561029457600080fd5b8201836020820111156102a657600080fd5b803590602001918460018302840111600160201b831117156102c757600080fd5b919350915035610698565b3480156102de57600080fd5b506102e7610716565b604080516001600160a01b039092168252519081900360200190f35b34801561030f57600080fd5b506101e76004803603608081101561032657600080fd5b6001600160a01b038235169190810190604081016020820135600160201b81111561035057600080fd5b82018360208201111561036257600080fd5b803590602001918460018302840111600160201b8311171561038357600080fd5b91908080601f01602080910402602001604051908101604052809392919081815260200183838082843760009201919091525092958435959094909350604081019250602001359050600160201b8111156103dd57600080fd5b8201836020820111156103ef57600080fd5b803590602001918460018302840111600160201b8311171561041057600080fd5b91908080601f016020809104026020016040519081016040528093929190818152602001838380828437600092019190915250929550610725945050505050565b34801561045d57600080fd5b506101e76004803603604081101561047457600080fd5b810190602081018135600160201b81111561048e57600080fd5b8201836020820111156104a057600080fd5b803590602001918460018302840111600160201b831117156104c157600080fd5b9193509150356108c3565b60018054604080516020600284861615610100026000190190941693909304601f810184900484028201840190925281815292918301828280156105515780601f1061052657610100808354040283529160200191610551565b820191906000526020600020905b81548152906001019060200180831161053457829003601f168201915b505050505081565b6000546001600160a01b031633148061058157503360009081526002602052604090205460ff165b6105bc5760405162461bcd60e51b815260040180806020018281038252602c815260200180610942602c913960400191505060405180910390fd5b6001600160a01b03166000908152600260205260409020805460ff19166001179055565b7f76e164bd47fa8c2042e7d654f6dd9803fc7fad3abdef4f517ee43e76f4b6a9aa3334848460405180856001600160a01b03168152602001848152602001806020018281038252848482818152602001925080828437600083820152604051601f909101601f191690920182900397509095505050505050a1600080546040516001600160a01b03909116913480156108fc02929091818181858888f19350505050158015610693573d6000803e3d6000fd5b505050565b7f0bfeac9b043dd3347852d34b3736af7f42fddc9370189899c4c5e019ff31150d3384848460405180856001600160a01b03168152602001806020018381526020018281038252858582818152602001925080828437600083820152604051601f909101601f191690920182900397509095505050505050a1505050565b6000546001600160a01b031681565b6000546001600160a01b031633148061074d57503360009081526002602052604090205460ff165b6107885760405162461bcd60e51b815260040180806020018281038252602c815260200180610942602c913960400191505060405180910390fd5b6000849050806001600160a01b031663fc38398b8585856040518463ffffffff1660e01b8152600401808060200184815260200180602001838103835286818151815260200191508051906020019080838360005b838110156107f55781810151838201526020016107dd565b50505050905090810190601f1680156108225780820380516001836020036101000a031916815260200191505b50838103825284518152845160209182019186019080838360005b8381101561085557818101518382015260200161083d565b50505050905090810190601f1680156108825780820380516001836020036101000a031916815260200191505b5095505050505050600060405180830381600087803b1580156108a457600080fd5b505af11580156108b8573d6000803e3d6000fd5b505050505050505050565b7fe10193894feb7423835e9f6b542ca46783b38e29ed2df225b350f5c13af9aadf3384848460405180856001600160a01b03168152602001806020018381526020018281038252858582818152602001925080828437600083820152604051601f909101601f191690920182900397509095505050505050a150505056fe596f7520617265206e6f7420746865206f776e6572206f6620746865206f7261636c6520636f6e7472616374a264697066735822122067c8a480e633eacfd3a6d07f4237f334e02bc6a8a540069808cf5759ff7493fd64736f6c63430007040033";
        public Oracle888730DeploymentBase() : base(BYTECODE) { }
        public Oracle888730DeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class AddOracleSecondaryAccountFunction : AddOracleSecondaryAccountFunctionBase { }

    [Function("AddOracleSecondaryAccount")]
    public class AddOracleSecondaryAccountFunctionBase : FunctionMessage
    {
        [Parameter("address", "_newOracleSecondaryAccount", 1)]
        public virtual string NewOracleSecondaryAccount { get; set; }
    }

    public partial class GetRequestFunction : GetRequestFunctionBase { }

    [Function("GetRequest")]
    public class GetRequestFunctionBase : FunctionMessage
    {
        [Parameter("string", "_service", 1)]
        public virtual string Service { get; set; }
        [Parameter("uint256", "_serviceType", 2)]
        public virtual BigInteger ServiceType { get; set; }
    }

    public partial class GetSubscribeRequestFunction : GetSubscribeRequestFunctionBase { }

    [Function("GetSubscribeRequest")]
    public class GetSubscribeRequestFunctionBase : FunctionMessage
    {
        [Parameter("string", "_service", 1)]
        public virtual string Service { get; set; }
        [Parameter("uint256", "_serviceType", 2)]
        public virtual BigInteger ServiceType { get; set; }
    }

    public partial class OracleNameFunction : OracleNameFunctionBase { }

    [Function("OracleName", "string")]
    public class OracleNameFunctionBase : FunctionMessage
    {

    }

    public partial class PayServiceFunction : PayServiceFunctionBase { }

    [Function("PayService")]
    public class PayServiceFunctionBase : FunctionMessage
    {
        [Parameter("string", "_service", 1)]
        public virtual string Service { get; set; }
    }

    public partial class SendResponseFunction : SendResponseFunctionBase { }

    [Function("SendResponse")]
    public class SendResponseFunctionBase : FunctionMessage
    {
        [Parameter("address", "_clientAddress", 1)]
        public virtual string ClientAddress { get; set; }
        [Parameter("string", "_service", 2)]
        public virtual string Service { get; set; }
        [Parameter("uint256", "_serviceType", 3)]
        public virtual BigInteger ServiceType { get; set; }
        [Parameter("string", "_value", 4)]
        public virtual string Value { get; set; }
    }

    public partial class OracleOwnerFunction : OracleOwnerFunctionBase { }

    [Function("oracleOwner", "address")]
    public class OracleOwnerFunctionBase : FunctionMessage
    {

    }

    public partial class GenericPaymentEventEventDTO : GenericPaymentEventEventDTOBase { }

    [Event("GenericPaymentEvent")]
    public class GenericPaymentEventEventDTOBase : IEventDTO
    {
        [Parameter("address", "_sender", 1, false )]
        public virtual string Sender { get; set; }
        [Parameter("uint256", "_value", 2, false )]
        public virtual BigInteger Value { get; set; }
    }

    public partial class RequestEventEventDTO : RequestEventEventDTOBase { }

    [Event("RequestEvent")]
    public class RequestEventEventDTOBase : IEventDTO
    {
        [Parameter("address", "_sender", 1, false )]
        public virtual string Sender { get; set; }
        [Parameter("string", "_requestService", 2, false )]
        public virtual string RequestService { get; set; }
        [Parameter("uint256", "_requestServiceType", 3, false )]
        public virtual BigInteger RequestServiceType { get; set; }
    }

    public partial class ServicePaymentEventEventDTO : ServicePaymentEventEventDTOBase { }

    [Event("ServicePaymentEvent")]
    public class ServicePaymentEventEventDTOBase : IEventDTO
    {
        [Parameter("address", "_sender", 1, false )]
        public virtual string Sender { get; set; }
        [Parameter("uint256", "_value", 2, false )]
        public virtual BigInteger Value { get; set; }
        [Parameter("string", "_serviceType", 3, false )]
        public virtual string ServiceType { get; set; }
    }

    public partial class SubscribeEventEventDTO : SubscribeEventEventDTOBase { }

    [Event("SubscribeEvent")]
    public class SubscribeEventEventDTOBase : IEventDTO
    {
        [Parameter("address", "_sender", 1, false )]
        public virtual string Sender { get; set; }
        [Parameter("string", "_subscribeService", 2, false )]
        public virtual string SubscribeService { get; set; }
        [Parameter("uint256", "_subscribeServiceType", 3, false )]
        public virtual BigInteger SubscribeServiceType { get; set; }
    }







    public partial class OracleNameOutputDTO : OracleNameOutputDTOBase { }

    [FunctionOutput]
    public class OracleNameOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }





    public partial class OracleOwnerOutputDTO : OracleOwnerOutputDTOBase { }

    [FunctionOutput]
    public class OracleOwnerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }
}
