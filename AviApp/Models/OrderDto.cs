/*
 * AviApp
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
 * 
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using AviApp.Converters;

namespace AviApp.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class OrderDto : IEquatable<OrderDto>
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id", EmitDefaultValue=true)]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or Sets OrderDate
        /// </summary>
        [DataMember(Name="orderDate", EmitDefaultValue=false)]
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Gets or Sets CustomerId
        /// </summary>
        [DataMember(Name="customerId", EmitDefaultValue=true)]
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or Sets OrderMenuItems
        /// </summary>
        [DataMember(Name="orderMenuItems", EmitDefaultValue=true)]
        public List<OrderMenuItemDto> OrderMenuItems { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OrderDto {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  OrderDate: ").Append(OrderDate).Append("\n");
            sb.Append("  CustomerId: ").Append(CustomerId).Append("\n");
            sb.Append("  OrderMenuItems: ").Append(OrderMenuItems).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((OrderDto)obj);
        }

        /// <summary>
        /// Returns true if OrderDto instances are equal
        /// </summary>
        /// <param name="other">Instance of OrderDto to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OrderDto other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Id == other.Id ||
                    Id != null &&
                    Id.Equals(other.Id)
                ) && 
                (
                    OrderDate == other.OrderDate ||
                    OrderDate != null &&
                    OrderDate.Equals(other.OrderDate)
                ) && 
                (
                    CustomerId == other.CustomerId ||
                    
                    CustomerId.Equals(other.CustomerId)
                ) && 
                (
                    OrderMenuItems == other.OrderMenuItems ||
                    OrderMenuItems != null &&
                    other.OrderMenuItems != null &&
                    OrderMenuItems.SequenceEqual(other.OrderMenuItems)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    if (Id != null)
                    hashCode = hashCode * 59 + Id.GetHashCode();
                    if (OrderDate != null)
                    hashCode = hashCode * 59 + OrderDate.GetHashCode();
                    
                    hashCode = hashCode * 59 + CustomerId.GetHashCode();
                    if (OrderMenuItems != null)
                    hashCode = hashCode * 59 + OrderMenuItems.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(OrderDto left, OrderDto right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(OrderDto left, OrderDto right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
